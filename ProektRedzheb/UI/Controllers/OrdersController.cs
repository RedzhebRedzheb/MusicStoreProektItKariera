using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore;
using MusicStore.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace UI.Controllers
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

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var orders = await _context.Orders
                .Where(o => o.UserId == user.Id)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (!User.IsInRole("Admin") && order.UserId != user.Id)
                return Forbid();

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");
            return View(new Order { OrderDate = DateTime.UtcNow });
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order, int[] productIds, int[] quantities)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            if (productIds.Length != quantities.Length)
                ModelState.AddModelError("", "Product and quantity arrays must match");

            if (ModelState.IsValid)
            {
                order.UserId = user.Id;
                order.OrderDate = DateTime.UtcNow;
                order.Status = "Pending";

                for (int i = 0; i < productIds.Length; i++)
                {
                    var product = await _context.Products.FindAsync(productIds[i]);
                    if (product == null) continue;

                    order.OrderItems.Add(new OrderItem
                    {
                        ProductId = productIds[i],
                        Quantity = quantities[i],
                        UnitPrice = product.Price
                    });
                }

                order.TotalPrice = order.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice);
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = order.Id });
            }
            ViewData["Products"] = new SelectList(_context.Products, "Id", "Name");
            return View(order);
        }

        // GET: Orders/Edit/5 (Admin only)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return View(order);
        }

        // POST: Orders/Edit/5 (Admin only)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status")] Order order)
        {
            if (id != order.Id) return NotFound();

            var existingOrder = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (existingOrder == null) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    existingOrder.Status = order.Status;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Details), new { id = order.Id });
            }
            return View(order);
        }

        // POST: Orders/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            if (!User.IsInRole("Admin") && order.UserId != user.Id)
                return Forbid();

            if (order.Status == "Shipped")
                ModelState.AddModelError("", "Cannot cancel shipped orders");

            if (ModelState.IsValid)
            {
                order.Status = "Cancelled";
                _context.Update(order);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        private bool OrderExists(int id) => _context.Orders.Any(e => e.Id == id);
    }
}
