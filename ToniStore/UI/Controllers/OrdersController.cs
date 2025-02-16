using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicStore.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly Contexts _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OrdersController(Contexts context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Shopping Cart Actions
        [Authorize]
        public async Task<IActionResult> Cart()
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = await GetOrCreateCartAsync(user);
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var user = await GetCurrentUserAsync();
            var product = await _context.Products.FindAsync(productId);

            if (product == null || product.StockQuantity < 1)
            {
                TempData["ErrorMessage"] = "Product unavailable";
                return RedirectToAction("Details", "Products", new { id = productId });
            }

            var cart = await GetOrCreateCartAsync(user);
            var existingItem = cart.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.OrderItems.Add(new OrderItem
                {
                    ProductId = productId,
                    Quantity = 1,
                    UnitPrice = product.Price
                });
            }

            UpdateCartTotals(cart);
            product.StockQuantity--;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"{product.Name} added to cart!";
            return RedirectToAction("Details", "Products", new { id = productId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var user = await GetCurrentUserAsync();
            var cart = await GetCartAsync(user);

            if (cart != null)
            {
                var item = cart.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);
                if (item != null)
                {
                    var product = await _context.Products.FindAsync(productId);
                    product.StockQuantity += item.Quantity;
                    cart.OrderItems.Remove(item);
                    UpdateCartTotals(cart);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Cart));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            // Authorization check
            if (!User.IsInRole("Admin") && order.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid();
            }

            return View(order);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null) return NotFound();

            if (!User.IsInRole("Admin") && order.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return Forbid();

            if (order.Status != "Pending")
            {
                TempData["ErrorMessage"] = "Only pending orders can be deleted";
                return RedirectToAction(nameof(Details), new { id });
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            if (order.Status != "Pending")
            {
                TempData["ErrorMessage"] = "Order cannot be deleted after processing";
                return RedirectToAction(nameof(Index));
            }

            // Restore stock
            foreach (var item in order.OrderItems)
            {
                item.Product.StockQuantity += item.Quantity;
                _context.Update(item.Product);
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Order deleted successfully";
            return RedirectToAction(nameof(Index));
        }
        // Inside your OrdersController class
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = await GetCartAsync(user);

            if (cart == null || !cart.OrderItems.Any())
            {
                return RedirectToAction(nameof(Cart));
            }

            // Convert cart to real order
            cart.Status = "Pending";
            cart.OrderDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = cart.Id });
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (User.IsInRole("Admin"))
            {
                // Admin view remains unchanged
                var adminOrders = await _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .Where(o => o.Status != "Cart") // Exclude carts
                    .OrderByDescending(o => o.OrderDate)
                    .ToListAsync();

                return View("AdminOrderList", adminOrders);
            }

            // Regular user view
            var userOrders = await _context.Orders
                .Where(o => o.UserId == userId && o.Status != "Cart") // Include cancelled orders in list
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            // Calculate total spending EXCLUDING cancelled orders
            ViewBag.TotalSpending = userOrders
                .Where(o => o.Status != "Cancelled")
                .Sum(o => o.TotalPrice);

            return View("UserOrderList", userOrders);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var validStatuses = new[] { "Pending", "Processing", "Shipped", "Delivered", "Cancelled" };

            if (!validStatuses.Contains(status))
            {
                TempData["ErrorMessage"] = "Invalid status value";
                return RedirectToAction(nameof(Details), new { id });
            }

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                TempData["ErrorMessage"] = "Order not found";
                return RedirectToAction(nameof(Index));
            }

            var previousStatus = order.Status;
            order.Status = status;

            // Handle stock changes for cancellations
            if (status == "Cancelled" && previousStatus != "Cancelled")
            {
                foreach (var item in order.OrderItems)
                {
                    item.Product.StockQuantity += item.Quantity;
                    _context.Update(item.Product);
                }
            }
            // Restore stock if undoing cancellation
            else if (previousStatus == "Cancelled" && status != "Cancelled")
            {
                foreach (var item in order.OrderItems)
                {
                    item.Product.StockQuantity = Math.Max(0, item.Product.StockQuantity - item.Quantity);
                    _context.Update(item.Product);
                }
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Order status updated successfully";
            return RedirectToAction(nameof(Details), new { id });
        }

        // Helper Methods
        private async Task<IdentityUser> GetCurrentUserAsync() =>
            await _userManager.GetUserAsync(User);

        private async Task<Order> GetCartAsync(IdentityUser user) =>
            await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.UserId == user.Id && o.Status == "Cart");

        private async Task<Order> GetOrCreateCartAsync(IdentityUser user)
        {
            var cart = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.UserId == user.Id && o.Status == "Cart");

            if (cart == null)
            {
                cart = new Order
                {
                    UserId = user.Id,
                    Status = "Cart",
                    OrderDate = DateTime.UtcNow,
                    TotalPrice = 0
                };
                _context.Orders.Add(cart);
                await _context.SaveChangesAsync();
            }
            return cart;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDelete(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null) return NotFound();

            return View("Delete", order);
        }

        [HttpPost, ActionName("AdminDelete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDeleteConfirmed(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            // Restore stock for non-cancelled orders
            if (order.Status != "Cancelled")
            {
                foreach (var item in order.OrderItems)
                {
                    item.Product.StockQuantity += item.Quantity;
                    _context.Update(item.Product);
                }
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Order permanently deleted";
            return RedirectToAction(nameof(AdminOrderList));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminOrderList()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }

        private void UpdateCartTotals(Order cart)
        {
            cart.TotalPrice = cart.OrderItems.Sum(oi => oi.UnitPrice * oi.Quantity);
        }
    }
}