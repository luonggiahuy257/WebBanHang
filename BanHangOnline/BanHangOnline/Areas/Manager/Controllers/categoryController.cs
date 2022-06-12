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
    public class categoryController : Controller
    {
        private readonly DataContext _context;

        public categoryController(DataContext context)
        {
            _context = context;
        }

        // GET: Manager/category
        public async Task<IActionResult> Index()
        {
              return View(await _context.categoryViewModel.ToListAsync());
        }

        // GET: Manager/category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.categoryViewModel == null)
            {
                return NotFound();
            }

            var categoryViewModel = await _context.categoryViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryViewModel == null)
            {
                return NotFound();
            }

            return View(categoryViewModel);
        }

        // GET: Manager/category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manager/category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentId,CategoryName,ShortDescription,Description,CategoryImages,CategoryURL,CategorySeoKeywords,CategoryShowHome,CategoryEnable,CategoryOrder,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] categoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryViewModel);
        }

        // GET: Manager/category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.categoryViewModel == null)
            {
                return NotFound();
            }

            var categoryViewModel = await _context.categoryViewModel.FindAsync(id);
            if (categoryViewModel == null)
            {
                return NotFound();
            }
            return View(categoryViewModel);
        }

        // POST: Manager/category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParentId,CategoryName,ShortDescription,Description,CategoryImages,CategoryURL,CategorySeoKeywords,CategoryShowHome,CategoryEnable,CategoryOrder,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] categoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!categoryViewModelExists(categoryViewModel.Id))
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
            return View(categoryViewModel);
        }

        // GET: Manager/category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.categoryViewModel == null)
            {
                return NotFound();
            }

            var categoryViewModel = await _context.categoryViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryViewModel == null)
            {
                return NotFound();
            }

            return View(categoryViewModel);
        }

        // POST: Manager/category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.categoryViewModel == null)
            {
                return Problem("Entity set 'DataContext.categoryViewModel'  is null.");
            }
            var categoryViewModel = await _context.categoryViewModel.FindAsync(id);
            if (categoryViewModel != null)
            {
                _context.categoryViewModel.Remove(categoryViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool categoryViewModelExists(int id)
        {
          return _context.categoryViewModel.Any(e => e.Id == id);
        }
    }
}
