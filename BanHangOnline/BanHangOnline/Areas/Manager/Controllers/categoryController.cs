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
using BanHangOnline.Areas.Manager.Models;
using Microsoft.AspNetCore.Authorization;

namespace BanHangOnline.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = ("Admin"))]
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
              return View(await _context.Category.OrderByDescending(item => item.Id).ToListAsync());
        }

        // GET: Manager/category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var CategoryViewModel = await _context.Category
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
            return View(MakeCategory());
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

                //if (postedFiles.Count == 0)
                //{
                //    return View();
                //}
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
                    };
                };

                var stringtitle = CategoryViewModel.CategoryName;
                var charsToRemove = new string[] { "@", ",", ".", ";", "'", "/", "?" };
                foreach (var c in charsToRemove)
                {
                    stringtitle = stringtitle.Replace(c, string.Empty);
                }

                for (int i = 1; i < VietNamChar.Length; i++)
                {
                    for (int j = 0; j < VietNamChar[i].Length; j++)
                    {
                        stringtitle = stringtitle.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                    }
                }

                stringtitle = stringtitle.Replace(" ", "-");

                CategoryViewModel.CategoryURL = stringtitle;
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

            if (id == null || _context.Category == null)
            {
                return NotFound();
            }
            CategoryCreate categoryCreate = MakeCategoryEdit();
            CategoryViewModel CategoryViewModel = await _context.Category.FindAsync(id);

            if (CategoryViewModel != null)
            {
                categoryCreate.Id = CategoryViewModel.Id;
                categoryCreate.ParentId = CategoryViewModel.ParentId;
                categoryCreate.CategoryName = CategoryViewModel.CategoryName;
                categoryCreate.ShortDescription = CategoryViewModel.ShortDescription;
                categoryCreate.Description = CategoryViewModel.Description;
                categoryCreate.CategoryImages = CategoryViewModel.CategoryImages;
                categoryCreate.CategoryURL = CategoryViewModel.CategoryURL;
                categoryCreate.CategorySeoKeywords = CategoryViewModel.CategorySeoKeywords;
                categoryCreate.CategoryShowHome = CategoryViewModel.CategoryShowHome;
                categoryCreate.CategoryEnable = CategoryViewModel.CategoryEnable;
                categoryCreate.CategoryOrder = CategoryViewModel.CategoryOrder;
                categoryCreate.CreatedAt = CategoryViewModel.CreatedAt;
                categoryCreate.CreatedBy = CategoryViewModel.CreatedBy;
                categoryCreate.UpdatedAt = CategoryViewModel.UpdatedAt;
                categoryCreate.UpdatedBy = CategoryViewModel.UpdatedBy;
            }

            if (CategoryViewModel == null)
            {
                return NotFound();
            }
            return View(categoryCreate);
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


                    var stringtitle = CategoryViewModel.CategoryName;
                    var charsToRemove = new string[] { "@", ",", ".", ";", "'", "/", "?" };
                    foreach (var c in charsToRemove)
                    {
                        stringtitle = stringtitle.Replace(c, string.Empty);
                    }
                    for (int i = 1; i < VietNamChar.Length; i++)
                    {
                        for (int j = 0; j < VietNamChar[i].Length; j++)
                            stringtitle = stringtitle.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                    }
                    stringtitle = stringtitle.Replace(" ", "-");



                    CategoryViewModel.CategoryURL = stringtitle;
                    CategoryViewModel.CreatedAt = _context.Category.Where(item => item.Id == CategoryViewModel.Id).Select(items => items.CreatedAt).FirstOrDefault();
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
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var CategoryViewModel = await _context.Category
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
            if (_context.Category == null)
            {
                return Problem("Entity set 'DataContext.CategoryViewModel'  is null.");
            }
            var CategoryViewModel = await _context.Category.FindAsync(id);
            if (CategoryViewModel != null)
            {
                _context.Category.Remove(CategoryViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryViewModelExists(int id)
        {
          return _context.Category.Any(e => e.Id == id);
        }


        private CategoryCreate MakeCategory()
        {
            List<CategoryViewModel> catergory = _context.Category.Where(item => item.ParentId == null).Select(x => new CategoryViewModel { Id = x.Id, CategoryName = x.CategoryName }).ToList();
            CategoryCreate webCategory = new CategoryCreate();
            if (catergory.Count != 0)
            {
                webCategory.Categorys = catergory;
            }

            return webCategory;
        }
        
        private CategoryCreate MakeCategoryEdit()
        {

            List<CategoryViewModel> catergory = _context.Category.Where(item => item.ParentId == null).Select(x => new CategoryViewModel { Id = x.Id, CategoryName = x.CategoryName }).ToList();
            CategoryCreate webCategory = new CategoryCreate();
            if (catergory.Count != 0)
            {
                webCategory.Categorys = catergory;
            }

            return webCategory;
        }
    }
}
