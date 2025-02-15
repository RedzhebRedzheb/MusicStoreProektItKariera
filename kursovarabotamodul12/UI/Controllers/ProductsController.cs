using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Models;
using System.IO;
using System.Linq;
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

        // GET: Products - Index with Filters and Search
        public async Task<IActionResult> Index(string search, int? category, int? brand, string sortOrder)
        {
            // Get the list of products and include related entities
            var products = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .AsQueryable();

            // Apply search filter if there's a search keyword
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search));
            }

            // Apply category filter if a category is selected
            if (category.HasValue)
            {
                products = products.Where(p => p.CategoryId == category.Value);
            }

            // Apply brand filter if a brand is selected
            if (brand.HasValue)
            {
                products = products.Where(p => p.BrandId == brand.Value);
            }

            // Sorting functionality (price, name, etc.)
            switch (sortOrder)
            {
                case "price_asc":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.Name); // Default sort by name
                    break;
            }

            // Prepare ViewBag for categories and brands to be used in the filters
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            ViewBag.Brands = new SelectList(await _context.Brands.ToListAsync(), "Id", "Name");

            return View(await products.ToListAsync());
        }

        // GET: Products/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);

            return product == null ? NotFound() : View(product);
        }

        // GET: Admin Create Product
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Suppliers = _context.Suppliers.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Product product, int categoryId, int brandId, int supplierId, IFormFile ImageUrl)
        {
            if (ModelState.IsValid)
            {
                product.CategoryId = categoryId;
                product.BrandId = brandId;
                product.SupplierId = supplierId;

                // Handle Image Upload
                if (ImageUrl != null && ImageUrl.Length > 0)
                {
                    var fileName = Path.GetFileName(ImageUrl.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    // Save the image file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageUrl.CopyToAsync(stream);
                    }

                    // Save the image URL to the product record
                    product.ImageUrl = "/images/" + fileName;
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Suppliers = _context.Suppliers.ToList();
            return View(product);
        }



        // GET: Admin Edit Product
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            // Prepare dropdowns for category, brand, and supplier
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Brands = new SelectList(_context.Brands, "Id", "Name");
            ViewBag.Suppliers = new SelectList(_context.Suppliers, "Id", "Name");

            return View(product);
        }

        // POST: Admin Edit Product
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Product product, int supplierId, int brandId, int categoryId)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existingProduct = await _context.Products.FindAsync(id);
                if (existingProduct == null) return NotFound();

                // Update the existing product
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.StockQuantity = product.StockQuantity;
                existingProduct.Description = product.Description;
                existingProduct.SupplierId = supplierId;
                existingProduct.BrandId = brandId;
                existingProduct.CategoryId = categoryId;

                // Save changes to the database
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Reload dropdowns for category, brand, and supplier if validation fails
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Brands = new SelectList(_context.Brands, "Id", "Name");
            ViewBag.Suppliers = new SelectList(_context.Suppliers, "Id", "Name");

            return View(product);
        }

        // GET: Admin Delete Product
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

            return View(product);
        }

        // POST: Admin Delete Product
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Check if product exists
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
