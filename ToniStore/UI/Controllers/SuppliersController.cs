using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SuppliersController : Controller
    {
        private readonly Contexts _context;

        public SuppliersController(Contexts context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Suppliers.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);

            return supplier == null ? NotFound() : View(supplier);
        }

        public IActionResult Create()
        {
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Products = _context.Products.ToList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Supplier supplier,
                            List<int> SelectedProductIds,
                            List<string> NewProductNames,
                            List<decimal> NewProductPrices,
                            List<int> NewProductQuantities,
                            List<int> SelectedBrandIds,
                            List<string> NewBrandNames,
                            List<int> SelectedCategoryIds,
                            List<string> NewCategoryNames)
        {
            if (ModelState.IsValid)
            {
                _context.Suppliers.Add(supplier);
                _context.SaveChanges(); 

                if (SelectedProductIds != null)
                {
                    foreach (var productId in SelectedProductIds)
                    {
                        var product = _context.Products.Find(productId);
                        if (product != null)
                        {
                            product.SupplierId = supplier.Id;
                        }
                    }
                }

                var createdBrandIds = new List<int>();
                if (NewBrandNames != null)
                {
                    foreach (var brandName in NewBrandNames)
                    {
                        var newBrand = new Brand { Name = brandName };
                        _context.Brands.Add(newBrand);
                        _context.SaveChanges();
                        createdBrandIds.Add(newBrand.Id);
                    }
                }

                var createdCategoryIds = new List<int>();
                if (NewCategoryNames != null)
                {
                    foreach (var categoryName in NewCategoryNames)
                    {
                        var newCategory = new Category { Name = categoryName };
                        _context.Categories.Add(newCategory);
                        _context.SaveChanges();
                        createdCategoryIds.Add(newCategory.Id);
                    }
                }

                if (NewProductNames != null)
                {
                    for (int i = 0; i < NewProductNames.Count; i++)
                    {
                        var newProduct = new Product
                        {
                            Name = NewProductNames[i],
                            Price = NewProductPrices[i],
                            StockQuantity = NewProductQuantities[i],
                            SupplierId = supplier.Id,
                            BrandId = SelectedBrandIds[i] > 0 ? SelectedBrandIds[i] : createdBrandIds[i], 
                            CategoryId = SelectedCategoryIds[i] > 0 ? SelectedCategoryIds[i] : createdCategoryIds[i] 
                        };
                        _context.Products.Add(newProduct);
                    }
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Products = _context.Products.ToList();
            return View(supplier);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var supplier = await _context.Suppliers.FindAsync(id);
            return supplier == null ? NotFound() : View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ContactInfo")] Supplier supplier)
        {
            if (id != supplier.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);

            if (supplier == null) return NotFound();

            ViewBag.CanDelete = !await _context.Products.AnyAsync(p => p.SupplierId == id);
            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (await _context.Products.AnyAsync(p => p.SupplierId == id))
            {
                ModelState.AddModelError("", "Cannot delete supplier with existing products");
                return View("Delete", supplier);
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(int id) => _context.Suppliers.Any(e => e.Id == id);
    }
}