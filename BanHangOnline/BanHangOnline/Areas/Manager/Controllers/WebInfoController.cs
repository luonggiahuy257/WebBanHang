using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BanHangOnline.Database;
using BanHangOnline.ViewModels;

namespace BanHangOnline.Areas.Managers.Controllers
{
    [Area("Manager")]
    public class WebInfoController : Controller
    {
        private readonly DataContext _context;

        public WebInfoController(DataContext context)
        {
            _context = context;
        }

        // GET: Managers/WebInfo
        public async Task<IActionResult> Index()
        {
              return View(await _context.WebInfo.ToListAsync());
        }

        // GET: Managers/WebInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WebInfo == null)
            {
                return NotFound();
            }

            var webInfoViewModels = await _context.WebInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webInfoViewModels == null)
            {
                return NotFound();
            }

            return View(webInfoViewModels);
        }

        // GET: Managers/WebInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Managers/WebInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageLogo,TitleLogo,Slogone,CompanyTitle,ShortDescription,Description,Address,PhoneNumber,Hotline,Hotline2,Email,Website,TitleWeb,MainKeywordH1,Seokeyword,Seodescription,SeoGooglesiteverification,SeoAuthor,SeoRevisitafter,Seorobots,Seogeoregion,Seogeoplacename,Seogeoposition,SeoICBM,Seomsvalidate01,Seocontentlanguage,SeoCOPYRIGHT,Seogoogleanalytics,Fax,Facebook,Twitter,Googleplus,Telegram,Skype,BaiViet1,BaiViet2,So1,So2")] WebInfoViewModels webInfoViewModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(webInfoViewModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(webInfoViewModels);
        }

        // GET: Managers/WebInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WebInfo == null)
            {
                return NotFound();
            }

            var webInfoViewModels = await _context.WebInfo.FindAsync(id);
            if (webInfoViewModels == null)
            {
                return NotFound();
            }
            return View(webInfoViewModels);
        }

        // POST: Managers/WebInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageLogo,TitleLogo,Slogone,CompanyTitle,ShortDescription,Description,Address,PhoneNumber,Hotline,Hotline2,Email,Website,TitleWeb,MainKeywordH1,Seokeyword,Seodescription,SeoGooglesiteverification,SeoAuthor,SeoRevisitafter,Seorobots,Seogeoregion,Seogeoplacename,Seogeoposition,SeoICBM,Seomsvalidate01,Seocontentlanguage,SeoCOPYRIGHT,Seogoogleanalytics,Fax,Facebook,Twitter,Googleplus,Telegram,Skype,BaiViet1,BaiViet2,So1,So2")] WebInfoViewModels webInfoViewModels)
        {
            if (id != webInfoViewModels.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webInfoViewModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebInfoViewModelsExists(webInfoViewModels.Id))
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
            return View(webInfoViewModels);
        }

        // GET: Managers/WebInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WebInfo == null)
            {
                return NotFound();
            }

            var webInfoViewModels = await _context.WebInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webInfoViewModels == null)
            {
                return NotFound();
            }

            return View(webInfoViewModels);
        }

        // POST: Managers/WebInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WebInfo == null)
            {
                return Problem("Entity set 'DataContext.WebInfo'  is null.");
            }
            var webInfoViewModels = await _context.WebInfo.FindAsync(id);
            if (webInfoViewModels != null)
            {
                _context.WebInfo.Remove(webInfoViewModels);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebInfoViewModelsExists(int id)
        {
          return _context.WebInfo.Any(e => e.Id == id);
        }
    }
}
