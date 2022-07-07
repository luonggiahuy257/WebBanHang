using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BanHangOnline.Database;
using BanHangOnline.ViewModels;
using Microsoft.Data.SqlClient;

namespace BanHangOnline.Areas.Managers.Controllers
{
    [Area("Manager")]
    public class WebBannerGroupController : Controller
    {
        private readonly DataContext _context;

        public WebBannerGroupController(DataContext context)
        {
            _context = context;
        }

        // GET: Managers/WebBannerGroup
        public async Task<IActionResult> Index()
        {
              return View(await _context.WebBannerGroup.ToListAsync());
        }

        // GET: Managers/WebBannerGroup/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WebBannerGroup == null)
            {
                return NotFound();
            }

            var webBannerGroupViewModel = await _context.WebBannerGroup
                .FirstOrDefaultAsync(m => m.BannerGroupID == id);
            if (webBannerGroupViewModel == null)
            {
                return NotFound();
            }

            return View(webBannerGroupViewModel);
        }

        // GET: Managers/WebBannerGroup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Managers/WebBannerGroup/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BannerGroupID,BannerGroupName,BannerGroupEnable")] WebBannerGroupViewModel webBannerGroupViewModel)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        WebBannerGroupViewModel webBannerGroupViewModels = new WebBannerGroupViewModel();
                        webBannerGroupViewModels.BannerGroupName = webBannerGroupViewModel.BannerGroupName;
                        webBannerGroupViewModels.BannerGroupEnable = webBannerGroupViewModel.BannerGroupEnable;

                         _context.WebBannerGroup.Add(webBannerGroupViewModels);
                        _context.SaveChanges();

                        transaction.Commit();

                        return View(nameof(Index));
                    }
                }
                catch (DbUpdateException e)
                 when (e.InnerException?.InnerException is SqlException sqlEx &&
                   (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                {

                    return View(webBannerGroupViewModel);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            return View(webBannerGroupViewModel);
        }

        // GET: Managers/WebBannerGroup/Edit/5
        //[Route("Managers/[controller]/[action]/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WebBannerGroup == null)
            {
                return NotFound();
            }

            var webBannerGroupViewModel = await _context.WebBannerGroup.FindAsync(id);
            if (webBannerGroupViewModel == null)
            {
                return NotFound();
            }
            return View(webBannerGroupViewModel);
        }

        // POST: Managers/WebBannerGroup/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BannerGroupID,BannerGroupName,BannerGroupEnable")] WebBannerGroupViewModel webBannerGroupViewModel)
        {
            if (id != webBannerGroupViewModel.BannerGroupID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webBannerGroupViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebBannerGroupViewModelExists(webBannerGroupViewModel.BannerGroupID))
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
            return View(webBannerGroupViewModel);
        }

        // GET: Managers/WebBannerGroup/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WebBannerGroup == null)
            {
                return NotFound();
            }

            var webBannerGroupViewModel = await _context.WebBannerGroup
                .FirstOrDefaultAsync(m => m.BannerGroupID == id);
            if (webBannerGroupViewModel == null)
            {
                return NotFound();
            }

            return View(webBannerGroupViewModel);
        }

        // POST: Managers/WebBannerGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WebBannerGroup == null)
            {
                return Problem("Entity set 'DataContext.WebBannerGroup'  is null.");
            }
            var webBannerGroupViewModel = await _context.WebBannerGroup.FindAsync(id);
            if (webBannerGroupViewModel != null)
            {
                _context.WebBannerGroup.Remove(webBannerGroupViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebBannerGroupViewModelExists(int id)
        {
          return _context.WebBannerGroup.Any(e => e.BannerGroupID == id);
        }
    }
}
