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
using static BanHangOnline.Common.WebConst;
using System.IO;

namespace BanHangOnline.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class categoryController : Controller
    {
        private readonly DataContext _context;
        private IWebHostEnvironment Environment;
        public categoryController(DataContext context, IWebHostEnvironment environment)
        {
            _context = context;
            Environment = environment;
        }

        // GET: Manager/category
        public async Task<IActionResult> Index()
        {
              return View(await _context.category.ToListAsync());
        }

        // GET: Manager/category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.category == null)
            {
                return NotFound();
            }

            var CategoryViewModel = await _context.category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CategoryViewModel == null)
            {
                return NotFound();
            }

            return View(CategoryViewModel);
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
        public async Task<IActionResult> Create([Bind("Id,ParentId,CategoryName,ShortDescription,Description,CategoryImages,CategoryURL,CategorySeoKeywords,CategoryShowHome,CategoryEnable,CategoryOrder,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] CategoryViewModel CategoryViewModel, List<IFormFile> postedFiles)
        {
            if (ModelState.IsValid)
            {
                DateTime now = System.DateTime.Now;

                if (postedFiles.Count == 0)
                {
                    return View();
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
                string path = Path.Combine(this.Environment.WebRootPath, FileSave.PathSaveCategory);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                List<string> uploadedFiles = new List<string>();
                foreach (IFormFile postedFile in postedFiles)
                {
                    if (postedFile.Length > 2097152)
                    {
                        return View(CategoryViewModel);
                    }

                    string nameext = Guid.NewGuid().ToString("N").Trim(charsToTrimg).ToLower().Substring(0, 12);
                    string flname = nameext + Path.GetExtension(postedFile.FileName).ToLower();
                    using (FileStream stream = new FileStream(Path.Combine(path, flname), FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                        uploadedFiles.Add(flname);
                        CategoryViewModel.CategoryImages = FileSave.PathCategory + flname;
                    }
                }

                CategoryViewModel.CreatedAt = now;
                CategoryViewModel.UpdatedAt = now;
                _context.Add(CategoryViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(CategoryViewModel);
        }

        // GET: Manager/category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.category == null)
            {
                return NotFound();
            }

            var CategoryViewModel = await _context.category.FindAsync(id);
            if (CategoryViewModel == null)
            {
                return NotFound();
            }
            return View(CategoryViewModel);
        }

        // POST: Manager/category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ParentId,CategoryName,ShortDescription,Description,CategoryImages,CategoryURL,CategorySeoKeywords,CategoryShowHome,CategoryEnable,CategoryOrder,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] CategoryViewModel CategoryViewModel, List<IFormFile> postedFiles)
        {
            if (id != CategoryViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    DateTime now = System.DateTime.Now;

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
                    string path = Path.Combine(this.Environment.WebRootPath, FileSave.PathSaveCategory);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    List<string> uploadedFiles = new List<string>();
                    foreach (IFormFile postedFile in postedFiles)
                    {
                        if (postedFile.Length > 2097152)
                        {
                            
                            return View(CategoryViewModel);
                        }

                        string nameext = Guid.NewGuid().ToString("N").Trim(charsToTrimg).ToLower().Substring(0, 12);
                        string flname = nameext + Path.GetExtension(postedFile.FileName).ToLower();
                        using (FileStream stream = new FileStream(Path.Combine(path, flname), FileMode.Create))
                        {
                            postedFile.CopyTo(stream);
                            uploadedFiles.Add(flname);
                            CategoryViewModel.CategoryImages = FileSave.PathCategory + flname;
                        }
                    }

                    CategoryViewModel.CreatedAt = _context.category.Where(item => item.Id == CategoryViewModel.Id).Select(items => items.CreatedAt).FirstOrDefault();
                    CategoryViewModel.UpdatedAt = now;
                    _context.Update(CategoryViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryViewModelExists(CategoryViewModel.Id))
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
            return View(CategoryViewModel);
        }

        // GET: Manager/category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.category == null)
            {
                return NotFound();
            }

            var CategoryViewModel = await _context.category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CategoryViewModel == null)
            {
                return NotFound();
            }

            return View(CategoryViewModel);
        }

        // POST: Manager/category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.category == null)
            {
                return Problem("Entity set 'DataContext.CategoryViewModel'  is null.");
            }
            var CategoryViewModel = await _context.category.FindAsync(id);
            if (CategoryViewModel != null)
            {
                _context.category.Remove(CategoryViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryViewModelExists(int id)
        {
          return _context.category.Any(e => e.Id == id);
        }
    }
}
