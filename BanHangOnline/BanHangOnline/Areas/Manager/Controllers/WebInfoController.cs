using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BanHangOnline.Database;
using BanHangOnline.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using static BanHangOnline.Common.WebConst;
using Microsoft.AspNetCore.Authorization;

namespace BanHangOnline.Areas.Managers.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = ("Admin"))]
    public class WebInfoController : Controller
    {
        private readonly DataContext _context;
        private IWebHostEnvironment Environment;
        public WebInfoController(DataContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            Environment = _environment;
        }

        // GET: Managers/WebInfo
        public async Task<IActionResult> Index()
        {
              return View(await _context.WebInfo.OrderByDescending(item => item.Id).ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Id,ImageLogo,TitleLogo,Slogone,CompanyTitle,ShortDescription,Description,Address,PhoneNumber,Hotline,Hotline2,Email,Website,TitleWeb,MainKeywordH1,Seokeyword,Seodescription,SeoGooglesiteverification,SeoAuthor,SeoRevisitafter,Seorobots,Seogeoregion,Seogeoplacename,Seogeoposition,SeoICBM,Seomsvalidate01,Seocontentlanguage,SeoCOPYRIGHT,Seogoogleanalytics,Fax,Facebook,Twitter,Googleplus,Telegram,Skype,BaiViet1,BaiViet2,So1,So2")] WebInfoViewModels webInfoViewModels, List<IFormFile> postedFiles, List<IFormFile> postedFilesAbout)
        {
            if (ModelState.IsValid)
            {
                if (postedFiles.Count == 0 || postedFilesAbout == null)
                {
                    return View(new WebInfoViewModels());
                }

                char[] charsToTrim = { ' ' };
                char[] charsToTrimg = { '-' };
                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;
                string path = Path.Combine(this.Environment.WebRootPath, FileSave.PathSaveWebInfo);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                List<string> uploadedFiles = new List<string>();
                foreach (IFormFile postedFile in postedFiles)
                {
                    if (postedFile.Length > 2097152)
                    {
                        return View(new WebInfoViewModels());
                    }

                    string nameext = Guid.NewGuid().ToString("N").Trim(charsToTrimg).ToLower().Substring(0, 12);
                    string flname = nameext + Path.GetExtension(postedFile.FileName).ToLower();
                    using (FileStream stream = new FileStream(Path.Combine(path, flname), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        uploadedFiles.Add(flname);
                        webInfoViewModels.ImageLogo = FileSave.PathWebInfo + flname;
                    }
                }

                foreach (IFormFile postedFile in postedFilesAbout)
                {
                    if (postedFile.Length > 2097152)
                    {
                        return View(new WebInfoViewModels());
                    }

                    string nameext = Guid.NewGuid().ToString("N").Trim(charsToTrimg).ToLower().Substring(0, 12);
                    string flname = nameext + Path.GetExtension(postedFile.FileName).ToLower();
                    using (FileStream stream = new FileStream(Path.Combine(path, flname), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        uploadedFiles.Add(flname);
                        webInfoViewModels.BaiViet1 = FileSave.PathWebInfo + flname;
                    }
                }

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageLogo,TitleLogo,Slogone,CompanyTitle,ShortDescription,Description,Address,PhoneNumber,Hotline,Hotline2,Email,Website,TitleWeb,MainKeywordH1,Seokeyword,Seodescription,SeoGooglesiteverification,SeoAuthor,SeoRevisitafter,Seorobots,Seogeoregion,Seogeoplacename,Seogeoposition,SeoICBM,Seomsvalidate01,Seocontentlanguage,SeoCOPYRIGHT,Seogoogleanalytics,Fax,Facebook,Twitter,Googleplus,Telegram,Skype,BaiViet1,BaiViet2,So1,So2")] WebInfoViewModels webInfoViewModels, List<IFormFile> postedFiles, List<IFormFile> postedFilesAbout)
        {
            if (id != webInfoViewModels.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    char[] charsToTrim = { ' ' };
                    char[] charsToTrimg = { '-' };
                    string wwwPath = this.Environment.WebRootPath;
                    string contentPath = this.Environment.ContentRootPath;
                    string path = Path.Combine(this.Environment.WebRootPath, FileSave.PathSaveWebInfo);

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    List<string> uploadedFiles = new List<string>();
                    foreach (IFormFile postedFile in postedFiles)
                    {
                        if (postedFile.Length > 2097152)
                        {
                            return View(new WebInfoViewModels());
                        }

                        string nameext = Guid.NewGuid().ToString("N").Trim(charsToTrimg).ToLower().Substring(0, 12);
                        string flname = nameext + Path.GetExtension(postedFile.FileName).ToLower();
                        using (FileStream stream = new FileStream(Path.Combine(path, flname), FileMode.Create))
                        {
                            postedFile.CopyTo(stream);
                            uploadedFiles.Add(flname);
                            webInfoViewModels.ImageLogo = FileSave.PathWebInfo + flname;
                        }
                    }

                    foreach (IFormFile postedFile in postedFilesAbout)
                    {
                        if (postedFile.Length > 2097152)
                        {
                            return View(new WebInfoViewModels());
                        }

                        string nameext = Guid.NewGuid().ToString("N").Trim(charsToTrimg).ToLower().Substring(0, 12);
                        string flname = nameext + Path.GetExtension(postedFile.FileName).ToLower();
                        using (FileStream stream = new FileStream(Path.Combine(path, flname), FileMode.Create))
                        {
                            postedFile.CopyTo(stream);
                            uploadedFiles.Add(flname);
                            webInfoViewModels.BaiViet1 = FileSave.PathWebInfo + flname;
                        }
                    }

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
