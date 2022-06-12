using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BanHangOnline.Database;
using BanHangOnline.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using static BanHangOnline.Common.WebConst;

namespace BanHangOnline.Areas.Managers.Controllers
{
    [Area("Manager")]
    public class WebBannerController : Controller
    {
        private readonly DataContext _context;
        private IWebHostEnvironment Environment;

        public WebBannerController(DataContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            Environment = _environment;
        }

        // GET: Managers/WebBannerViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebBanner.ToListAsync());
        }

        // GET: Managers/WebBannerViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WebBanner == null)
            {
                return NotFound();
            }

            var webBannerViewModel = await _context.WebBanner
                .FirstOrDefaultAsync(m => m.BannerId == id);
            if (webBannerViewModel == null)
            {
                return NotFound();
            }

            return View(webBannerViewModel);
        }

        // GET: Managers/WebBannerViewModels/Create
        public IActionResult Create()
        {
            return View(MakeWebBanner());
        }

        // POST: Managers/WebBannerViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BannerId,BannerImage,BannerTitle,BannerDescription,BannerAlt,BannerURL,BannerEnable,BannerGroupId,PostParentId")] WebBannerViewModel webBannerViewModel, List<IFormFile> postedFiles)
        {
            if (ModelState.IsValid)
            {
                if (postedFiles.Count == 0)
                {
                    return View(MakeWebBanner());
                }
                string[] VietNamChar = new string[]
                {
                    "aAeEoOuUiIdDyY",
                    "áàạảãâấầậẩẫăắằặẳẵ",
                    "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                    "éèẹẻẽêếềệểễ",
                    "ÉÈẸẺẼÊẾỀỆỂỄ",
                    "óòọỏõôốồộổỗơớờợởỡ",
                    "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                    "úùụủũưứừựửữ",
                    "ÚÙỤỦŨƯỨỪỰỬỮ",
                    "íìịỉĩ",
                    "ÍÌỊỈĨ",
                    "đ",
                    "Đ",
                    "ýỳỵỷỹ",
                    "ÝỲỴỶỸ"
                };

                char[] charsToTrim = { ' ' };
                char[] charsToTrimg = { '-' };
                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;
                string path = Path.Combine(this.Environment.WebRootPath, FileSave.PathSaveWebBanner);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                List<string> uploadedFiles = new List<string>();
                foreach (IFormFile postedFile in postedFiles)
                {
                    if (postedFile.Length > 2097152 )
                    {
                        return View(MakeWebBanner());
                    }

                    string nameext = Guid.NewGuid().ToString("N").Trim(charsToTrimg).ToLower().Substring(0, 12);
                    string flname = nameext + Path.GetExtension(postedFile.FileName).ToLower();
                    using (FileStream stream = new FileStream(Path.Combine(path, flname), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        uploadedFiles.Add(flname);
                        webBannerViewModel.BannerImage = FileSave.PathWebBanner + flname;
                    }
                }

                _context.Add(webBannerViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(MakeWebBanner());
        }

        // GET: Managers/WebBannerViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WebBanner == null)
            {
                return NotFound();
            }

            var webBannerViewModel = await _context.WebBanner.FindAsync(id);
            if (webBannerViewModel == null)
            {
                return NotFound();
            }

            List<WebBannerGroupViewModel> webBannerGroup = _context.WebBannerGroup.Select(x => new WebBannerGroupViewModel { BannerGroupID = x.BannerGroupID, BannerGroupName = x.BannerGroupName }).ToList();
            WebBannerViewModel BannerViewModel = new WebBannerViewModel();
            if (BannerViewModel != null)
            {
                webBannerViewModel.WebBannerGroup = webBannerGroup;
            }

            return View(webBannerViewModel);
        }

        // POST: Managers/WebBannerViewModels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BannerId,BannerImage,BannerTitle,BannerDescription,BannerAlt,BannerURL,BannerEnable,BannerGroupId,PostParentId")] WebBannerViewModel webBannerViewModel, List<IFormFile> postedFiles)
        {
            if (id != webBannerViewModel.BannerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (postedFiles.Count == 0)
                    {
                        return View(MakeWebBanner());
                    }
                    string[] VietNamChar = new string[]
                    {
                    "aAeEoOuUiIdDyY",
                    "áàạảãâấầậẩẫăắằặẳẵ",
                    "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                    "éèẹẻẽêếềệểễ",
                    "ÉÈẸẺẼÊẾỀỆỂỄ",
                    "óòọỏõôốồộổỗơớờợởỡ",
                    "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                    "úùụủũưứừựửữ",
                    "ÚÙỤỦŨƯỨỪỰỬỮ",
                    "íìịỉĩ",
                    "ÍÌỊỈĨ",
                    "đ",
                    "Đ",
                    "ýỳỵỷỹ",
                    "ÝỲỴỶỸ"
                    };

                    char[] charsToTrim = { ' ' };
                    char[] charsToTrimg = { '-' };
                    string wwwPath = this.Environment.WebRootPath;
                    string contentPath = this.Environment.ContentRootPath;
                    string path = Path.Combine(this.Environment.WebRootPath, FileSave.PathSaveWebBanner);

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    List<string> uploadedFiles = new List<string>();
                    foreach (IFormFile postedFile in postedFiles)
                    {
                        if (postedFile.Length > 2097152)
                        {
                            return View(MakeWebBanner());
                        }

                        string nameext = Guid.NewGuid().ToString("N").Trim(charsToTrimg).ToLower().Substring(0, 12);
                        string flname = nameext + Path.GetExtension(postedFile.FileName).ToLower();
                        using (FileStream stream = new FileStream(Path.Combine(path, flname), FileMode.Create))
                        {
                            postedFile.CopyTo(stream);
                            uploadedFiles.Add(flname);
                            webBannerViewModel.BannerImage = FileSave.PathWebBanner + flname;
                        }
                    }

                    _context.Update(webBannerViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebBannerViewModelExists(webBannerViewModel.BannerId))
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

            return View(MakeWebBanner());
        }

        // GET: Managers/WebBannerViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WebBanner == null)
            {
                return NotFound();
            }

            var webBannerViewModel = await _context.WebBanner
                .FirstOrDefaultAsync(m => m.BannerId == id);
            if (webBannerViewModel == null)
            {
                return NotFound();
            }

            return View(webBannerViewModel);
        }

        // POST: Managers/WebBannerViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WebBanner == null)
            {
                return Problem("Entity set 'DataContext.WebBanner'  is null.");
            }
            var webBannerViewModel = await _context.WebBanner.FindAsync(id);
            if (webBannerViewModel != null)
            {
                _context.WebBanner.Remove(webBannerViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebBannerViewModelExists(int id)
        {
            return _context.WebBanner.Any(e => e.BannerId == id);
        }

        private WebBannerViewModel MakeWebBanner()
        {
            List<WebBannerGroupViewModel> webBannerGroup = _context.WebBannerGroup.Select(x => new WebBannerGroupViewModel { BannerGroupID = x.BannerGroupID, BannerGroupName = x.BannerGroupName }).ToList();
            WebBannerViewModel webBannerViewModel = new WebBannerViewModel();
            if (webBannerGroup.Count != 0)
            {
                webBannerViewModel.WebBannerGroup = webBannerGroup;
            }

            return webBannerViewModel;
        }
    }
}
