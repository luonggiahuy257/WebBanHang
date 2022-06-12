using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BanHangOnline.Database;
using BanHangOnline.ViewModels;

namespace BanHangOnline.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        // GET: Manager/Product
        public async Task<IActionResult> Index()
        {
              return View(await _context.ProductViewModel.ToListAsync());
        }

        // GET: Manager/Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductViewModel == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.ProductViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // GET: Manager/Product/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Manager/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,SKU,ShortDescription,Description,ProductSeoKeywords,ManufacturerId,UnitTypeId,TaxRateId,SalePrice,RetailPrice,Point,ShowOnHomePage,ShowOnSalePage,WarehouseId,ProductTypeId,IsGroup,OwenSale,QuantityInStock,DisplayOrder,Published,Deleted,ProductTitleURL,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // GET: Manager/Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductViewModel == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.ProductViewModel.FindAsync(id);
            if (productViewModel == null)
            {
                return NotFound();
            }
            return View(productViewModel);
        }

        // POST: Manager/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,SKU,ShortDescription,Description,ProductSeoKeywords,ManufacturerId,UnitTypeId,TaxRateId,SalePrice,RetailPrice,Point,ShowOnHomePage,ShowOnSalePage,WarehouseId,ProductTypeId,IsGroup,OwenSale,QuantityInStock,DisplayOrder,Published,Deleted,ProductTitleURL,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductViewModelExists(productViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // GET: Manager/Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductViewModel == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.ProductViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // POST: Manager/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductViewModel == null)
            {
                return Problem("Entity set 'DataContext.ProductViewModel'  is null.");
            }
            var productViewModel = await _context.ProductViewModel.FindAsync(id);
            if (productViewModel != null)
            {
                _context.ProductViewModel.Remove(productViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductViewModelExists(int id)
        {
          return _context.ProductViewModel.Any(e => e.Id == id);
        }
    }
}
