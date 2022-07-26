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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BanHangOnline.Common;

namespace BanHangOnline.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DataContext _context;

        public ProductController(ILogger<ProductController> logger, DataContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Route("chi-tiet-san-pham/{ProductDetailUrl}")]
        public IActionResult ProductDetail(string ProductDetailUrl)
        {
            List<ProductViewModel> ProductLists = _context.Product
                .Where(item => item.Published == true && item.ShowOnHomePage)
                .OrderByDescending(item => item.CreatedAt)
                .ToList();
            ProductViewModel Product = _context.Product.Where(item => item.ProductTitleURL == ProductDetailUrl).FirstOrDefault();
            if (Product != null)
            {
                ProductImageViewModel image = _context.ProductImage.Where(item => item.ProductId == Product.Id).First();
                List<ProductImageViewModel> images = _context.ProductImage.Where(item => item.ProductId == Product.Id).ToList();

                dynamic mymodel = new ExpandoObject();
                mymodel.ProductLists = ProductLists;
                mymodel.Products = Product;
                mymodel.productImages = images;
                mymodel.productImage = image;
                return View(mymodel);
            }
            return View();
        }
        public IActionResult ProductCategory()
        {
            List<ProductViewModel> Products = _context.Product.Where(item => item.Published == true && item.ShowOnHomePage)
                .OrderByDescending(item => item.CreatedAt)
                .ToList();
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
            mymodel.ListPagination = this.GetProduct(1, Products.Count());

            return View(mymodel);
        }

        [HttpPost("/danh-muc-san-pham")]
        public IActionResult ProductCategory(int currentPageIndex)
        {
            List<ProductViewModel> Products = _context.Product
                .Where(item => item.Published == true && item.ShowOnHomePage)
                .OrderByDescending(item => item.CreatedAt)
                .ToList();
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
            mymodel.ListPagination = this.GetProduct(currentPageIndex, Products.Count());

            return View(mymodel);
        }

        [Route("/danh-muc-san-pham-{categoryUrl}")]
        public IActionResult ProductList(string categoryUrl, int? currentPageIndex)
        {
            ViewBag.categoryUrl = categoryUrl;
            int currentIndex = 0;
            if (currentPageIndex == null)
            {
                currentIndex = 1;
            }
            else
            {
                currentIndex = (int)currentPageIndex;
            }

            CategoryViewModel Category = _context.Category.Where(item => item.CategoryURL == categoryUrl).FirstOrDefault();
            if (Category != null)
            {
                List<ProductViewModel> products = _context.Product
                    .Where(item => item.CategoryId == Category.Id)
                    .OrderByDescending(item => item.CreatedAt).ToList();
                List<ProductImageViewModel> productImages = new List<ProductImageViewModel>();
                foreach (ProductViewModel product in products)
                {
                    ProductImageViewModel productImage = new ProductImageViewModel();
                    ProductImageViewModel images = _context.ProductImage.Where(item => item.ProductId == product.Id).First();
                    productImage = images;
                    productImages.Add(productImage);
                }

                dynamic mymodel = new ExpandoObject();
                mymodel.Products = products;
                mymodel.productImages = productImages;
                mymodel.ListPagination = this.GetProductCategory(currentIndex, Category.Id, products.Count);

                return View(mymodel);
            }
            return View(new ExpandoObject());
        }

        [Route("Gio-hang")]
        [Authorize]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        [Authorize]
        [Route("/san-pham-yeu-thich")]
        public IActionResult WishList()
        {
            string userId = _userManager.GetUserId(User);
            List<WishProductViewModel> WishProduct = _context.WishProduct
                .Where(item => item.IdUser == userId)
                .OrderByDescending(item => item.Id)
                .ToList();

            List<ProductViewModel> Products = new List<ProductViewModel>();
            dynamic mymodel = new ExpandoObject();
            foreach (var itemWishProduct in WishProduct)
            {
                ProductViewModel Product = _context.Product.Where(item => item.Id == itemWishProduct.IdProduct).First();
                Products.Add(Product);
            }

            List<ProductImageViewModel> productImages = new List<ProductImageViewModel>();

            if (Products != null)
            {
                foreach (ProductViewModel product in Products)
                {
                    ProductImageViewModel productImage = new ProductImageViewModel();
                    ProductImageViewModel images = _context.ProductImage.Where(item => item.ProductId == product.Id).First();
                    productImage = images;
                    productImages.Add(productImage);
                }

                mymodel.Products = Products;
                mymodel.productImages = productImages;

                return View(mymodel);
            }

            mymodel.Products = new List<ProductViewModel>();
            mymodel.productImages = productImages;
            return View(mymodel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult WishList(int productid)
        {
            string userId = _userManager.GetUserId(User);
            List<WishProductViewModel> WishProductFind = _context.WishProduct
                .Where(item => item.IdUser == userId && item.IdProduct == productid)
                .OrderByDescending(item => item.Id)
                .ToList();

            if (WishProductFind.Count > 0)
            {
                return NotFound();
            }
            WishProductViewModel WishProduct = new WishProductViewModel();



            if (userId != null)
            {
                WishProduct.IdProduct = productid;
                WishProduct.IdUser = userId;
                WishProduct.IsEnable = true;
                _context.WishProduct.Add(WishProduct);
                _context.SaveChanges();
            }
            else
            {
                return PartialView("Error");
            }

            return Ok();
        }

        [Authorize]
        public IActionResult RemoveWishList(int id)
        {
            string userId = _userManager.GetUserId(User);

            WishProductViewModel Product = _context.WishProduct.Where(item => item.IdProduct == id && item.IdUser == userId).FirstOrDefault();

            if (Product != null)
            {
                _context.WishProduct.Remove(Product);
                _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(WishList));
        }

        [Authorize]
        public IActionResult CheckOut()
        {
            return View(GetCartItems());
        }
        [Authorize]
        [HttpPost]
        public IActionResult CheckOut(string LastName, string Address, string PhoneNumber, string Email,string Contents, float total)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        DateTime now = DateTime.Now;
                        List<CartViewModel> CartItemViewModel = GetCartItems();
                        CartViewModelCheckOut cartViewModel = new CartViewModelCheckOut();
                        cartViewModel.Id = func.GetIdProduct();
                        cartViewModel.LastName = LastName;
                        cartViewModel.Address = Address;
                        cartViewModel.Status = true;
                        cartViewModel.OrderStatusId = 1;
                        cartViewModel.PhoneNumber = PhoneNumber;
                        cartViewModel.Quantity = 999;
                        cartViewModel.Email = Email;
                        cartViewModel.Contents = Contents;
                        cartViewModel.CreatedAt = now;
                        cartViewModel.UpdatedAt = now;
                        _context.Cart.Add(cartViewModel);
                      
                        foreach (CartViewModel item in CartItemViewModel)
                        {
                            CartItemsViewModel cartItems = new CartItemsViewModel();
                            cartItems.ProductId = item.product.Id;
                            cartItems.CartId = cartViewModel.Id;
                            cartItems.Quantity = item.Quantity;
                            cartItems.Price = total;
                            cartItems.Active = false;
                            cartItems.CreatedAt = now;
                            cartItems.UpdatedAt = now;

                            _context.CartItems.Add(cartItems);
                        }
                        _context.SaveChanges();
                        transaction.Commit();
                        ClearCart();
                        return View(nameof(SuccessCheckOut));
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                return View(GetCartItems());
            }
        }
        [Authorize]
        public IActionResult SuccessCheckOut()
        {
            return View();
        }

        public IActionResult SalePage()
        {
            List<ProductViewModel> Products = _context.Product
                .Where(item => item.Published == true && item.ShowOnHomePage == true && item.ShowOnSalePage == true && item.OwenSale == true)
                .OrderByDescending(item => item.CreatedAt)
                .ToList();
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
            mymodel.ListPagination = this.GetProductSalePage(1, Products.Count);

            return View(mymodel);
        }

        [HttpPost("/san-pham-khuyen-mai")]
        public IActionResult SalePage(int currentPageIndex)
        {
            List<ProductViewModel> Products = _context.Product
                .Where(item => item.Published == true && item.ShowOnHomePage == true && item.ShowOnSalePage == true && item.OwenSale == true)
                .OrderByDescending(item => item.CreatedAt)
                .ToList();
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
            mymodel.ListPagination = this.GetProduct(currentPageIndex, Products.Count());

            return View(mymodel);
        }

        #region PhanTrang
        private PaginationModel GetProduct(int currentPage, int productCount)
        {
            int maxRows = 4;
            PaginationModel productList = new PaginationModel();
            productList.Products = (from customer in this._context.Product
                                    select customer)
                    .Where(item => item.Published == true && item.ShowOnHomePage)
                    .OrderByDescending(customer => customer.CreatedAt)
                    .Skip((currentPage - 1) * maxRows)
                    .Take(maxRows).ToList();

            double pageCount = (double)((decimal)productCount / Convert.ToDecimal(maxRows));
            productList.PageCount = (int)Math.Ceiling(pageCount);

            productList.CurrentPageIndex = currentPage;

            return productList;
        }

        private PaginationModel GetProductSalePage(int currentPage, int productCount)
        {
            int maxRows = 4;
            PaginationModel productList = new PaginationModel();
            productList.Products = (from customer in this._context.Product
                                    select customer)
                    .Where(item => item.Published == true && item.ShowOnHomePage == true && item.ShowOnSalePage == true && item.OwenSale == true)
                    .OrderByDescending(customer => customer.CreatedAt)
                    .Skip((currentPage - 1) * maxRows)
                    .Take(maxRows).ToList();

            double pageCount = (double)((decimal)productCount / Convert.ToDecimal(maxRows));
            productList.PageCount = (int)Math.Ceiling(pageCount);

            productList.CurrentPageIndex = currentPage;

            return productList;
        }

        private PaginationModel GetProductCategory(int currentPage, int id, int productCount)
        {
            int maxRows = 4;

            PaginationModel productList = new PaginationModel();
            productList.Products = (from customer in this._context.Product
                                    select customer)
                    .Where(item => item.CategoryId == id).ToList()
                    .OrderByDescending(customer => customer.CreatedAt)
                    .Skip((currentPage - 1) * maxRows)
                    .Take(maxRows).ToList();

            double pageCount = (double)((decimal)productCount / Convert.ToDecimal(maxRows));
            productList.PageCount = (int)Math.Ceiling(pageCount);

            productList.CurrentPageIndex = currentPage;

            return productList;
        }
        #endregion


        #region secsion
        /// Thêm sản phẩm vào cart
        [Route("gio-hang/{productid:int}", Name = "addcart")]
        [Authorize]
        public IActionResult AddToCart([FromRoute] int productid, int? Quantity)
        {

            int quantity = 1;
            if (Quantity != null)
            {
                quantity = (int)Quantity;
            }


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
                cartitem.Quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartViewModel() { Quantity = quantity, product = product });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction(nameof(Cart));
        }

        /// xóa item trong cart
        [Route("/xoa-san-pham/{productid:int}", Name = "removecart")]
        [Authorize]
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
        [Route("/sua-san-pham", Name = "updatecart")]
        [Authorize]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.Id == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity = quantity;
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
        #endregion
    }
}
