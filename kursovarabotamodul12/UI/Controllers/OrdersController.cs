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

        // Index action - Cart for users, Orders list for admins
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                // Admin: Show all orders with user and product details
                var orders = _context.Orders
                    .Include(o => o.User)  // Include user details
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)  // Include product details in order items
                    .ToList();
                return View("AdminOrderList", orders);
            }

            // User: Show the user's orders (with updated status)
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userOrders = _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToList();
            return View("UserOrderList", userOrders);
        }

        // Admin view of the details of an order
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null) return NotFound();

            return View("AdminOrderDetails", order);
        }

        // Admin action to update the order status
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            order.Status = status;  // Update the order status
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = order.Id }); // Redirect back to the order details page
        }

        // Helper method to check if an order exists
        private bool OrderExists(int id) => _context.Orders.Any(e => e.Id == id);
    }
}
