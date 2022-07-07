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

namespace BanHangOnline.Areas.Manager.Controllers
{
    [Area("Manager")]
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
            return View(await _context.Product.ToListAsync());
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

            return View();
        }

        // POST: Manager/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("Id,ProductName,SKU,ShortDescription,Description,ProductSeoKeywords,ManufacturerId,UnitTypeId,TaxRateId,SalePrice,RetailPrice,Point,IsWish,ShowOnHomePage,ShowOnSalePage,WarehouseId,ProductTypeId,IsGroup,OwenSale,QuantityInStock,DisplayOrder,Published,Deleted,ProductTitleURL,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] ProductViewModel productViewModel, List<IFormFile> postedFiles)
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
                       
                        productViewModel.Id = func.GetIdProduct();
                        productViewModel.ProductTitleURL = func.GetUrlName(productViewModel.ProductName);
                        productViewModel.CreatedAt = now;
                        productViewModel.UpdatedAt = now;
                        productViewModel.UpdatedBy = "admin";
                        productViewModel.CreatedBy = "admin";

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

                        _context.Product.Add(productViewModel);
                        _context.SaveChanges();
                        transaction.Commit();

                        return RedirectToAction(nameof(Index));
                    }

                }
                catch (Exception)
                {
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

            var productViewModel = await _context.Product.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,SKU,ShortDescription,Description,ProductSeoKeywords,ManufacturerId,UnitTypeId,TaxRateId,SalePrice,RetailPrice,Point,IsWish,ShowOnHomePage,ShowOnSalePage,WarehouseId,ProductTypeId,IsGroup,OwenSale,QuantityInStock,DisplayOrder,Published,Deleted,ProductTitleURL,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] ProductViewModel productViewModel)
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
    }
}
