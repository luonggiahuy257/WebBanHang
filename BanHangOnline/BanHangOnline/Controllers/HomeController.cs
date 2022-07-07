using BanHangOnline.Database;
using BanHangOnline.Models;
using BanHangOnline.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace BanHangOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<WebBannerViewModel> webanners = _context.WebBanner.Where(item => item.BannerEnable == true).ToList();
            List<ProductViewModel> Products = _context.Product.Where(item => item.Published == true && item.ShowOnHomePage == true).ToList();
            List<CategoryViewModel> Categorys = _context.category.Where(item => item.CategoryEnable == true && item.CategoryShowHome == true).ToList();
            CategoryViewModel Category = _context.category.Where(item => item.CategoryEnable == true && item.CategoryShowHome == true).FirstOrDefault();
            List<ProductImageViewModel> productImages = new List<ProductImageViewModel>();

            foreach (ProductViewModel product in Products)
            {
                ProductImageViewModel productImage = new ProductImageViewModel();
                ProductImageViewModel images = _context.ProductImage.Where(item => item.ProductId == product.Id).First();
                productImage = images;
                productImages.Add(productImage);
            }

            dynamic mymodel = new ExpandoObject();
            mymodel.WebBanners = webanners;
            mymodel.Products = Products;
            mymodel.Categorys = Categorys;
            mymodel.Category = Category;
            mymodel.productImages = productImages;

            return View(mymodel);
        }

        public IActionResult About()
        {
            WebInfoViewModels webinfo = _context.WebInfo.Where(item => item.Id == 1).FirstOrDefault();
            return View(webinfo);
        }
        public IActionResult Contact()
        {
            WebInfoViewModels webinfo = _context.WebInfo.Where(item => item.Id == 1).FirstOrDefault();
            return View(webinfo);
        }

        [HttpPost]
        public ActionResult Contact([Bind("Id,Name,FullName,PhoneNumber,Email,Contents,ShortContent,ContactImages,ContactURL,IsGroup,ContactEnable,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contact.Add(contact);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult Services()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult HeaderHome()
        {
            List<CategoryViewModel> listCategory = _context.category.Where(item => item.CategoryEnable == true).ToList();

            return View(listCategory);
        }

        //public IActionResult HeaderHome()
        //{
        //    WebInfoViewModels webinfo = _context.WebInfo.Where(item => item.Id == 1).FirstOrDefault();
        //    return View(webinfo);
        //}

  
        public JsonResult GetCategorys()
        {
            try
            {
                var categorys = _context.category.Where(item => item.CategoryEnable == true).ToList();

                return new JsonResult(new { data = categorys });
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}
