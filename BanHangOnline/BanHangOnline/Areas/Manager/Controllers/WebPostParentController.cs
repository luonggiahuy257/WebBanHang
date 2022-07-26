using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BanHangOnline.Database;
using BanHangOnline.ViewModels;
using BanHangOnline.Areas.Manager.Models;
using BanHangOnline.Common;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BanHangOnline.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = ("Admin"))]
    public class WebPostParentController : Controller
    {
        private readonly DataContext _context;

        public WebPostParentController(DataContext context)
        {
            _context = context;
        }

        // GET: Manager/WebPostParent
        public async Task<IActionResult> Index()
        {
              return View(await _context.WebPostParent.OrderByDescending(item => item.PostParentID).ToListAsync());
        }

        // GET: Manager/WebPostParent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WebPostParent == null)
            {
                return NotFound();
            }

            var webPostParentViewModel = await _context.WebPostParent
                .FirstOrDefaultAsync(m => m.PostParentID == id);
            if (webPostParentViewModel == null)
            {
                return NotFound();
            }

            return View(webPostParentViewModel);
        }

        // GET: Manager/WebPostParent/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manager/WebPostParent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WebPostParentViewModel webPostParentViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                DateTime now = System.DateTime.Now;
                webPostParentViewModel.PostParentTitleURL = func.GetUrlName(webPostParentViewModel.PostParentTitle);

                webPostParentViewModel.CreatedAt = now;
                webPostParentViewModel.CreatedBy = userId;
                webPostParentViewModel.UpdatedAt = now;
                webPostParentViewModel.UpdatedBy = userId;
                _context.Add(webPostParentViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(webPostParentViewModel);
        }

        // GET: Manager/WebPostParent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WebPostParent == null)
            {
                return NotFound();
            }

            var webPostParentViewModel = await _context.WebPostParent.FindAsync(id);
            if (webPostParentViewModel == null)
            {
                return NotFound();
            }
            return View(webPostParentViewModel);
        }

        // POST: Manager/WebPostParent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostParentID,PostParentTitle,PostParentLowerTitle,PostParentReadmore,PostParentContent,PostParentImage,PostParentEnable,PostParentSeoKeyword,PostParentTitleURL,PostParentTitleGoogle,PostParentTitleImage,PostParentTitleAlt,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] WebPostParentViewModel webPostParentViewModel)
        {
            if (id != webPostParentViewModel.PostParentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    DateTime now = System.DateTime.Now;

                    webPostParentViewModel.PostParentTitleURL = func.GetUrlName(webPostParentViewModel.PostParentTitle);
                    webPostParentViewModel.CreatedAt = now;
                    webPostParentViewModel.CreatedBy = userId;
                    webPostParentViewModel.UpdatedAt = now;
                    webPostParentViewModel.UpdatedBy = userId;
                    _context.Update(webPostParentViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebPostParentViewModelExists(webPostParentViewModel.PostParentID))
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
            return View(webPostParentViewModel);
        }

        // GET: Manager/WebPostParent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WebPostParent == null)
            {
                return NotFound();
            }

            var webPostParentViewModel = await _context.WebPostParent
                .FirstOrDefaultAsync(m => m.PostParentID == id);
            if (webPostParentViewModel == null)
            {
                return NotFound();
            }

            return View(webPostParentViewModel);
        }

        // POST: Manager/WebPostParent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WebPostParent == null)
            {
                return Problem("Entity set 'DataContext.WebPostParent'  is null.");
            }
            var webPostParentViewModel = await _context.WebPostParent.FindAsync(id);
            if (webPostParentViewModel != null)
            {
                _context.WebPostParent.Remove(webPostParentViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebPostParentViewModelExists(int id)
        {
          return _context.WebPostParent.Any(e => e.PostParentID == id);
        }
    }
}
