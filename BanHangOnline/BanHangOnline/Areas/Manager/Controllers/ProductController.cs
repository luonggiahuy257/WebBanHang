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
using BanHangOnline.Common;
using BanHangOnline.Areas.Manager.Models;
using Microsoft.AspNetCore.Authorization;

namespace BanHangOnline.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = ("Admin"))]
    public class ProductController : Controller
    {
        private readonly DataContext _context;
        private IWebHostEnvironment Environment;
        public ProductController(DataContext context, IWebHostEnvironment environment)
        {
            _context = context;
            Environment = environment;
        }

        // GET: Manager/Product
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.OrderByDescending(item => item.CreatedAt).ToListAsync());
        }

        // GET: Manager/Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.Product
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

            return View(MakeCategoryEdit());
        }

        // POST: Manager/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(ProductViewModel productViewModel, List<IFormFile> postedFiles)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    if (ModelState.IsValid)
                    {
                        DateTime now = System.DateTime.Now;

                        if (postedFiles.Count == 0)
                        {
                            return View();
                        }
                        //string[] VietNamChar = new string[]
                        //{
                        //    "aAeEoOuUiIdDyY",
                        //    "áàạảãâấầậẩẫăắằặẳẵ",
                        //    "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                        //    "éèẹẻẽêếềệểễ",
                        //    "ÉÈẸẺẼÊẾỀỆỂỄ",
                        //    "óòọỏõôốồộổỗơớờợởỡ",
                        //    "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                        //    "úùụủũưứừựửữ",
                        //    "ÚÙỤỦŨƯỨỪỰỬỮ",
                        //    "íìịỉĩ",
                        //    "ÍÌỊỈĨ",
                        //    "đ",
                        //    "Đ",
                        //    "ýỳỵỷỹ",
                        //    "ÝỲỴỶỸ"
                        //};

                        char[] charsToTrim = { ' ' };
                        char[] charsToTrimg = { '-' };
                        string wwwPath = this.Environment.WebRootPath;
                        string contentPath = this.Environment.ContentRootPath;
                        string path = Path.Combine(this.Environment.WebRootPath, FileSave.PathSaveProduct);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        List<string> uploadedFiles = new List<string>();
                        List<ProductImageViewModel> productImageViewModel = new List<ProductImageViewModel>();
                       
                        productViewModel.Id = func.GetIdProduct();
                        productViewModel.ProductTitleURL = func.GetUrlName(productViewModel.ProductName);
                        productViewModel.CreatedAt = now;
                        productViewModel.UpdatedAt = now;
                        productViewModel.UpdatedBy = "admin";
                        productViewModel.CreatedBy = "admin";
                        productViewModel.TaxRateId = 1;

                        foreach (IFormFile postedFile in postedFiles)
                        {
                            ProductImageViewModel productImage = new ProductImageViewModel();
                            // File hình ảnh không được lớn hơn 2mb
                            if (postedFile.Length > 2097152)
                            {
                                return View(productViewModel);
                            }

                            string nameext = Guid.NewGuid().ToString("N").Trim(charsToTrimg).ToLower().Substring(0, 12);
                            string flname = nameext + Path.GetExtension(postedFile.FileName).ToLower();
                            using (FileStream stream = new FileStream(Path.Combine(path, flname), FileMode.Create))
                            {
                                postedFile.CopyTo(stream);
                                uploadedFiles.Add(flname);

                                productImage.FilePath = FileSave.PathProduct + flname;
                                productImage.FileName = flname;
                                productImage.ImageName = productViewModel.ProductName;
                                productImage.ProductId = productViewModel.Id;
                                productImage.Published = true;
                                productImageViewModel.Add(productImage);
                            }
                        }

                        foreach (ProductImageViewModel productImage in productImageViewModel)
                        {
                            _context.ProductImage.Add(productImage);
                        }

                        //var stringtitle = productViewModel.ProductName;
                        //var charsToRemove = new string[] { "@", ",", ".", ";", "'", "/", "?" };
                        //foreach (var c in charsToRemove)
                        //{
                        //    stringtitle = stringtitle.Replace(c, string.Empty);
                        //}
                        //for (int i = 1; i < VietNamChar.Length; i++)
                        //{
                        //    for (int j = 0; j < VietNamChar[i].Length; j++)
                        //        stringtitle = stringtitle.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                        //}
                        //stringtitle = stringtitle.Replace(" ", "-");
                        //productViewModel.ProductTitleURL = stringtitle;

                        _context.Product.Add(productViewModel);
                        _context.SaveChanges();
                        transaction.Commit();

                        return RedirectToAction(nameof(Index));
                    }

                }
                catch (Exception ex)
                {
                    var exxx = ex.Message;
                    transaction.Rollback();
                }
            }
            return View(productViewModel);
        }

        // GET: Manager/Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }
            CategoryDropdown categoryDropdown = MakeCategoryEdit();
            var productViewModel = await _context.Product.FindAsync(id);
            List<ProductImageViewModel> productImages = await _context.ProductImage.Where(item => item.ProductId == id).ToListAsync();
            if (productImages != null)
            {
                categoryDropdown.ProductImages = productImages;
            }
           
            if (productViewModel == null)
            {
                return NotFound();
            }

            else
            {
                categoryDropdown.ProductName = productViewModel.ProductName;
                categoryDropdown.SKU = productViewModel.SKU;
                categoryDropdown.ShortDescription = productViewModel.ShortDescription;
                categoryDropdown.Description = productViewModel.Description;
                categoryDropdown.ProductSeoKeywords = productViewModel.ProductSeoKeywords;
                categoryDropdown.ManufacturerId = productViewModel.ManufacturerId;
                categoryDropdown.Color = productViewModel.Color;
                categoryDropdown.Size = productViewModel.Size;
                categoryDropdown.UnitTypeId = productViewModel.UnitTypeId;
                categoryDropdown.TaxRateId = productViewModel.TaxRateId;
                categoryDropdown.SalePrice = productViewModel.SalePrice;
                categoryDropdown.RetailPrice = productViewModel.RetailPrice;
                categoryDropdown.Point = productViewModel.Point;
                categoryDropdown.IsWish = productViewModel.IsWish;
                categoryDropdown.ShowOnHomePage = productViewModel.ShowOnHomePage;
                categoryDropdown.ShowOnSalePage = productViewModel.ShowOnSalePage;
                categoryDropdown.WarehouseId = productViewModel.WarehouseId;
                categoryDropdown.ProductTypeId = productViewModel.ProductTypeId;
                categoryDropdown.IsGroup = productViewModel.IsGroup;
                categoryDropdown.OwenSale = productViewModel.OwenSale;
                categoryDropdown.QuantityInStock = productViewModel.QuantityInStock;
                categoryDropdown.DisplayOrder = productViewModel.DisplayOrder;
                categoryDropdown.Published = productViewModel.Published;
                categoryDropdown.Deleted = productViewModel.Deleted;
                categoryDropdown.ProductTitleURL = productViewModel.ProductTitleURL;
                categoryDropdown.CreatedAt = productViewModel.CreatedAt;
                categoryDropdown.CreatedBy = productViewModel.CreatedBy;
                categoryDropdown.UpdatedAt = productViewModel.UpdatedAt;
                categoryDropdown.UpdatedBy = productViewModel.UpdatedBy;
                categoryDropdown.CategoryId = productViewModel.CategoryId;
            }
            return View(categoryDropdown);
        }

        // POST: Manager/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel productViewModel, List<IFormFile> postedFiles)
        {
            if (id != productViewModel.Id)
            {
                return NotFound();
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        DateTime now = System.DateTime.Now;

                        if (postedFiles.Count == 0)
                        {
                            return View(new ProductViewModel());
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
                        string path = Path.Combine(this.Environment.WebRootPath, FileSave.PathSaveProduct);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        List<string> uploadedFiles = new List<string>();
                        List<ProductImageViewModel> productImageViewModel = new List<ProductImageViewModel>();

                        productViewModel.Id = id;
                        productViewModel.ProductTitleURL = func.GetUrlName(productViewModel.ProductName);
                        productViewModel.CreatedAt = now;
                        productViewModel.UpdatedAt = now;
                        productViewModel.UpdatedBy = "admin";
                        productViewModel.CreatedBy = "admin";
                        productViewModel.TaxRateId = 1;

                        foreach (IFormFile postedFile in postedFiles)
                        {
                            ProductImageViewModel productImage = new ProductImageViewModel();
                            // File hình ảnh không được lớn hơn 2mb
                            if (postedFile.Length > 2097152)
                            {
                                return View(productViewModel);
                            }

                            string nameext = Guid.NewGuid().ToString("N").Trim(charsToTrimg).ToLower().Substring(0, 12);
                            string flname = nameext + Path.GetExtension(postedFile.FileName).ToLower();
                            using (FileStream stream = new FileStream(Path.Combine(path, flname), FileMode.Create))
                            {
                                postedFile.CopyTo(stream);
                                uploadedFiles.Add(flname);

                                productImage.FilePath = FileSave.PathProduct + flname;
                                productImage.FileName = flname;
                                productImage.ImageName = productViewModel.ProductName;
                                productImage.ProductId = productViewModel.Id;
                                productImage.Published = true;
                                productImageViewModel.Add(productImage);
                            }
                        }

                        List<ProductImageViewModel> productImageListRemoves = await _context.ProductImage.Where(m => m.ProductId == id).ToListAsync();
                        if (productImageListRemoves != null)
                        {
                            foreach (var item in productImageListRemoves)
                            {
                                _context.ProductImage.Remove(item);
                            }

                        }
                        foreach (ProductImageViewModel productImage in productImageViewModel)
                        {
                            _context.ProductImage.Add(productImage);
                        }

                        var stringtitle = productViewModel.ProductName;
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
                        productViewModel.ProductTitleURL = stringtitle;

                        _context.Update(productViewModel);
                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        if (!ProductViewModelExists(productViewModel.Id))
                        {
                            transaction.Rollback();
                            return NotFound();
                        }
                        else
                        {
                            transaction.Rollback();
                            var Message = ex.Message;
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }

               
            return View(new ProductViewModel());
        }

        // GET: Manager/Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var productViewModel = await _context.Product
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
            if (_context.Product == null)
            {
                return Problem("Entity set 'DataContext.ProductViewModel'  is null.");
            }
            var productViewModel = await _context.Product.FindAsync(id);
            if (productViewModel != null)
            {
                _context.Product.Remove(productViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductViewModelExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        private CategoryDropdown MakeCategoryEdit()
        {

            List<CategoryViewModel> catergory = _context.Category.Where(item => item.ParentId != null && item.CategoryEnable == true).Select(x => new CategoryViewModel { Id = x.Id, CategoryName = x.CategoryName }).ToList();
            CategoryDropdown webCategory = new CategoryDropdown();
            if (catergory.Count != 0)
            {
                webCategory.Categorys = catergory;
            }

            return webCategory;
        }
    }
}
