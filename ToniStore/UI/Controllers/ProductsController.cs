using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Contexts _context;

        public ProductsController(Contexts context)
        {
            _context = context;
        }

        // GET: Products
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString, int? categoryId, int? brandId, string sortOrder)
        {
            var query = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.Name.Contains(searchString) ||
                       p.Description.Contains(searchString));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            if (brandId.HasValue)
            {
                query = query.Where(p => p.BrandId == brandId);
            }

            // Sorting
            ViewData["CurrentSort"] = sortOrder;
            query = sortOrder switch
            {
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                "name_asc" => query.OrderBy(p => p.Name),
                "name_desc" => query.OrderByDescending(p => p.Name),
                _ => query.OrderBy(p => p.Name)
            };

            // ViewData for form persistence
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentCategory"] = categoryId;
            ViewData["CurrentBrand"] = brandId;


            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["Brands"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name", categoryId);
            ViewData["Brands"] = new SelectList(_context.Brands, "Id", "Name", brandId);
            return View(await query.ToListAsync());
        }

        // GET: Products/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Include(p => p.Reviews)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Suppliers = new SelectList(_context.Suppliers, "Id", "Name");
            ViewBag.ExistingBrands = _context.Brands.Select(b => b.Name).ToList();
            ViewBag.ExistingCategories = _context.Categories.Select(c => c.Name).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Get or create brand
                    var brand = await _context.Brands
                        .FirstOrDefaultAsync(b => b.Name.ToLower() == model.BrandName.Trim().ToLower());

                    if (brand == null)
                    {
                        brand = new Brand { Name = model.BrandName.Trim() };
                        _context.Brands.Add(brand);
                        await _context.SaveChangesAsync();
                    }

                    // Get or create category
                    var category = await _context.Categories
                        .FirstOrDefaultAsync(c => c.Name.ToLower() == model.CategoryName.Trim().ToLower());

                    if (category == null)
                    {
                        category = new Category { Name = model.CategoryName.Trim() };
                        _context.Categories.Add(category);
                        await _context.SaveChangesAsync();
                    }

                    // Create product
                    var product = new Product
                    {
                        Name = model.Name,
                        BrandId = brand.Id,
                        CategoryId = category.Id,
                        SupplierId = model.SupplierId,
                        Price = model.Price,
                        StockQuantity = model.StockQuantity,
                        Description = model.Description,
                        ImageUrl = model.ImageUrl
                    };

                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "Error saving product. Please try again.");
                }
            }

            // Repopulate needed data if validation fails
            ViewBag.Suppliers = new SelectList(_context.Suppliers, "Id", "Name");
            ViewBag.ExistingBrands = _context.Brands.Select(b => b.Name).ToList();
            ViewBag.ExistingCategories = _context.Categories.Select(c => c.Name).ToList();
            return View(model);
        }

        private void PopulateDropdowns()
        {
            ViewBag.Brands = new SelectList(_context.Brands, "Id", "Name");
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Suppliers = new SelectList(_context.Suppliers, "Id", "Name");
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            PopulateDropdowns(product);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
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
            PopulateDropdowns(product);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            ViewBag.CanDelete = !await _context.OrderItems.AnyAsync(oi => oi.ProductId == id) &&
                              !await _context.Reviews.AnyAsync(r => r.ProductId == id);

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (await _context.OrderItems.AnyAsync(oi => oi.ProductId == id) ||
                await _context.Reviews.AnyAsync(r => r.ProductId == id))
            {
                ModelState.AddModelError("", "Cannot delete product with existing orders or reviews");
                return RedirectToAction(nameof(Delete), new { id });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(int productId, int rating, string comment)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get the current user's ID

                var review = new Review
                {
                    ProductId = productId,
                    UserId = userId,
                    Rating = rating,
                    Comment = comment
                };

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id = productId });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var review = await _context.Reviews
                .Include(r => r.Product)
                .FirstOrDefaultAsync(r => r.Id == reviewId);

            if (review == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Get the current user's ID

            // Allow deletion if the user is the commenter or an admin
            if (review.UserId == userId || User.IsInRole("Admin"))
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id = review.Product.Id });
        }
        private bool ProductExists(int id) => _context.Products.Any(e => e.Id == id);

        private void PopulateDropdowns(Product product = null)
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Name", product?.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product?.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", product?.SupplierId);
        }
    }
}