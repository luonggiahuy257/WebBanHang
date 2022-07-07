using BanHangOnline.Database;
using BanHangOnline.Models;
using BanHangOnline.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BanHangOnline.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly DataContext _context;

        public ProductController(ILogger<ProductController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

     
        [Route("chi-tiet-san-pham/{ProductDetailUrl}")]
        public IActionResult ProductDetail(string ProductDetailUrl)
        {
            List<ProductViewModel> ProductLists = _context.Product.Where(item => item.Published == true && item.ShowOnHomePage).ToList();
            ProductViewModel Product = _context.Product.Where(item => item.ProductTitleURL == ProductDetailUrl).FirstOrDefault();

            ProductImageViewModel image = _context.ProductImage.Where(item => item.ProductId == Product.Id).First();
            List<ProductImageViewModel> images = _context.ProductImage.Where(item => item.ProductId == Product.Id).ToList();

            dynamic mymodel = new ExpandoObject();
            mymodel.ProductLists = ProductLists;
            mymodel.Products = Product;
            mymodel.productImages = images;
            mymodel.productImage = image;
            return View(mymodel);
        }
        public IActionResult ProductCategory()
        {
           
            List<ProductViewModel> Products = _context.Product.Where(item => item.Published == true && item.ShowOnHomePage).ToList();
            List<ProductImageViewModel> productImages = new List<ProductImageViewModel>();

            foreach (ProductViewModel product in Products)
            {
                ProductImageViewModel productImage = new ProductImageViewModel();
                ProductImageViewModel images = _context.ProductImage.Where(item => item.ProductId == product.Id).First();
                productImage = images;
                productImages.Add(productImage);
            }

            dynamic mymodel = new ExpandoObject();
            mymodel.Products = Products;
            mymodel.productImages = productImages;
            mymodel.ListPagination = this.GetProduct(1);

            return View(mymodel);
        }

        [HttpPost("/danh-muc-san-pham/{currentPageIndex:int?}/")]
        public IActionResult ProductCategory(int currentPageIndex)
        {
            List<ProductViewModel> Products = _context.Product.Where(item => item.Published == true && item.ShowOnHomePage).ToList();
            List<ProductImageViewModel> productImages = new List<ProductImageViewModel>();

            foreach (ProductViewModel product in Products)
            {
                ProductImageViewModel productImage = new ProductImageViewModel();
                ProductImageViewModel images = _context.ProductImage.Where(item => item.ProductId == product.Id).First();
                productImage = images;
                productImages.Add(productImage);
            }

            dynamic mymodel = new ExpandoObject();
            mymodel.Products = Products;
            mymodel.productImages = productImages;
            mymodel.ListPagination = this.GetProduct(currentPageIndex);

            return View(mymodel);
        }

        public IActionResult ProductList()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View(GetCartItems());
        }
        public IActionResult WishList()
        {
            List<ProductViewModel> Products = _context.Product.Where(item => item.IsWish == true).ToList();
            List<ProductImageViewModel> productImages = new List<ProductImageViewModel>();

            foreach (ProductViewModel product in Products)
            {
                ProductImageViewModel productImage = new ProductImageViewModel();
                ProductImageViewModel images = _context.ProductImage.Where(item => item.ProductId == product.Id).First();
                productImage = images;
                productImages.Add(productImage);
            }

            dynamic mymodel = new ExpandoObject();
            mymodel.Products = Products;
            mymodel.productImages = productImages;
            
            return View(mymodel);
        }

        [HttpPost]
        public IActionResult WishList(int productid)
        {
            ProductViewModel Products = _context.Product.Where(item => item.Id == productid).FirstOrDefault();
            ProductViewModel Product = null;
            if (Product != null)
            {
                Product.IsWish = true;
                _context.SaveChanges();
            }
            else
            {
                return PartialView("Error");
            }
            
            return Ok();
        }

        [HttpPost]
        public IActionResult RemoveWishList(int productid)
        {
            ProductViewModel Products = _context.Product.Where(item => item.Id == productid).FirstOrDefault();
            ProductViewModel Product = null;
            if (Product != null)
            {
                Product.IsWish = false;
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return View(nameof(WishList));
        }


        public IActionResult CheckOut()
        {
            return View();
        }

        private PaginationModel GetProduct(int currentPage)
        {
            int maxRows = 4;

            PaginationModel productList = new PaginationModel();
            productList.Products = (from customer in this._context.Product
                                    select customer)
                    .Where(item => item.Published == true && item.ShowOnHomePage)
                    .OrderBy(customer => customer.Id)
                    .Skip((currentPage - 1) * maxRows)
                    .Take(maxRows).ToList();

            double pageCount = (double)((decimal)this._context.Product.Count() / Convert.ToDecimal(maxRows));
            productList.PageCount = (int)Math.Ceiling(pageCount);

            productList.CurrentPageIndex = currentPage;

            return productList;
        }

        /// Thêm sản phẩm vào cart
        [Route("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] int productid)
        {

            var product = _context.Product
                .Where(p => p.Id == productid)
                .FirstOrDefault();
            if (product == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.Id == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartViewModel() { quantity = 1, product = product });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction(nameof(Cart));
        }

        /// xóa item trong cart
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.Id == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

        /// Cập nhật
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.Id == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }

        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        List<CartViewModel> GetCartItems()
        {
            var session = HttpContext.Session;
            string valueString = session.GetString("key");

            session.SetString("yourkey", "yourstring");
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartViewModel>>(jsoncart);
            }
            return new List<CartViewModel>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartViewModel> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
    }
}
