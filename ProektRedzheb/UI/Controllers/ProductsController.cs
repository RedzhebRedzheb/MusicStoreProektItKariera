using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MusicStore.Contexts _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductsController(MusicStore.Contexts context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .AsNoTracking()
                .ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue) return NotFound();

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Include(p => p.Reviews)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            return product == null ? NotFound() : View(product);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(product);
                return View(product);
            }

            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            PopulateDropdowns(product);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                PopulateDropdowns(product);
                return View(product);
            }

            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return NotFound();

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            return product == null ? NotFound() : View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult CreateReview(int productId)
        {
            ViewData["ProductId"] = productId;
            return View(new Review { ProductId = productId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateReview(Review review)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            review.UserId = user.Id;
            review.ReviewDate = DateTime.UtcNow;

            if (!ModelState.IsValid)
            {
                ViewData["ProductId"] = review.ProductId;
                return View(review);
            }

            _context.Add(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = review.ProductId });
        }

        [Authorize]
        public async Task<IActionResult> EditReview(int? id)
        {
            if (!id.HasValue) return NotFound();

            var review = await _context.Reviews
                .Include(r => r.Product)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (!User.IsInRole("Admin") && review.UserId != user.Id)
                return Forbid();

            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditReview(int id, Review review)
        {
            if (id != review.Id) return NotFound();

            var existingReview = await _context.Reviews
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            var user = await _userManager.GetUserAsync(User);
            if (!User.IsInRole("Admin") && existingReview?.UserId != user.Id)
                return Forbid();

            if (!ModelState.IsValid) return View(review);

            try
            {
                review.UserId = existingReview.UserId;
                review.ReviewDate = existingReview.ReviewDate;
                _context.Update(review);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(review.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Details), new { id = review.ProductId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews
                .Include(r => r.Product)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (!User.IsInRole("Admin") && review.UserId != user.Id)
                return Forbid();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = review.ProductId });
        }

        private void PopulateDropdowns(Product product = null)
        {
            ViewBag.BrandId = new SelectList(_context.Brands, "Id", "Name", product?.BrandId);
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", product?.CategoryId);
            ViewBag.SupplierId = new SelectList(_context.Suppliers, "Id", "Name", product?.SupplierId);
        }

        private bool ProductExists(int id) => _context.Products.Any(e => e.Id == id);
        private bool ReviewExists(int id) => _context.Reviews.Any(e => e.Id == id);
    }
}