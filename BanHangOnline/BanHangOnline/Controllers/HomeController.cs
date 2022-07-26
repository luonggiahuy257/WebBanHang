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
            List<ProductViewModel> Products = _context.Product.Where(item => item.Published == true).OrderByDescending(item => item.Id).ToList();
            List<CategoryViewModel> Categorys = _context.Category.Where(item => item.CategoryEnable == true && item.CategoryShowHome == true).ToList();
            CategoryViewModel Category = _context.Category.Where(item => item.CategoryEnable == true && item.CategoryShowHome == true).FirstOrDefault();
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

        [Route("lien-he")]
        public IActionResult Contact()
        {
            WebInfoViewModels webinfo = _context.WebInfo.Where(item => item.Id == 1).FirstOrDefault();
            return View(webinfo);
        }

        [HttpPost]
        [Route("lien-he")]
        public ActionResult Contact([Bind("Id,Name,FullName,PhoneNumber,Email,Contents,ShortContent,ContactImages,ContactURL,IsGroup,ContactEnable,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy")] ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                DateTime now = System.DateTime.Now;
                contact.CreatedAt = now;
                contact.UpdatedAt = now;
                contact.UpdatedBy = "khach";
                contact.CreatedBy = "khach";
                contact.IsGroup = false;
                contact.ContactEnable = true;

                _context.Contact.Add(contact);
                _context.SaveChanges();

                return RedirectToAction(nameof(ContactSuccess));
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
            List<CategoryViewModel> listCategory = _context.Category.Where(item => item.CategoryEnable == true).ToList();

            return View(listCategory);
        }

        public IActionResult ErrorPage()
        {

            return View();
        }

        //public IActionResult HeaderHome()
        //{
        //    WebInfoViewModels webinfo = _context.WebInfo.Where(item => item.Id == 1).FirstOrDefault();
        //    return View(webinfo);
        //}

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Uh oh, this page could not be found";
                    ViewBag.ErrorNumber = "404";
                    break;
                case 405:
                    ViewBag.ErrorMessage = "Method not allowed. Contact administrator";
                    ViewBag.ErrorNumber = "405";
                    break;
                case 401:
                    ViewBag.ErrorMessage = "You do not have acccess to this page. Please make sure you are logged in, or contact your administrator.";
                    ViewBag.ErrorNumber = "401";
                    break;
                case 500:
                    ViewBag.ErrorMessage = "Internal Server Error. Please contact administrator.";
                    ViewBag.ErrorNumber = "500";
                    break;
                case 403:
                    ViewBag.ErrorMessage = "Forbidden. Please contact administrator.";
                    ViewBag.ErrorNumber = "403";
                    break;
                case 503:
                    ViewBag.ErrorMessage = "Service unavailable. Please contact administrator";
                    ViewBag.ErrorNumber = "503";
                    break;
                case 504:
                    ViewBag.ErrorMessage = "Gateway Timeout. Please contact administrator";
                    ViewBag.ErrorNumber = "504";
                    break;
                case 001:
                    ViewBag.ErrorMessage = "This link has expired.";
                    ViewBag.ErrorNumber = "Oh no!";
                    break;

            }
            return View("NotFound");

        }
        public JsonResult GetCategorys()
        {
            try
            {
                var categorys = _context.Category.Where(item => item.CategoryEnable == true).ToList();

                return new JsonResult(new { data = categorys });
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public ActionResult ContactSuccess()
        {
            return View();
        }

    }
}
