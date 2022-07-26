using BanHangOnline.Database;
using BanHangOnline.Models;
using BanHangOnline.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace BanHangOnline.Controllers
{
    public class SearchController : Controller
    {
        private readonly DataContext _context;

        public SearchController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Index(string SearchString)
        {
            try
            {
                if (string.IsNullOrEmpty(SearchString))
                {
                    return View();
                }

                ViewBag.SearchStr = SearchString;
                List<ProductViewModel> Products = _context.Product.Where(item => item.ProductName.ToUpper().Contains(SearchString.ToUpper())).ToList();
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
                mymodel.ListPagination = this.GetProduct(1, SearchString, Products.Count());

                return View(mymodel);
            }
            catch (System.Exception)
            {

                return null;
            }
        }

        [HttpPost("/tim-kiem-san-pham/{currentPageIndex:int?}")]
        public IActionResult SearchForm(string SearchString, int currentPageIndex)
        {
            try
            {
                List<ProductViewModel> Products = _context.Product.Where(item => item.ProductName.ToUpper().Contains(SearchString.ToUpper())).ToList();
                List<ProductImageViewModel> productImages = new List<ProductImageViewModel>();

                foreach (ProductViewModel product in Products)
                {
                    ProductImageViewModel productImage = new ProductImageViewModel();
                    ProductImageViewModel images = _context.ProductImage.Where(item => item.ProductId == product.Id).First();
                    productImage = images;
                    productImages.Add(productImage);
                }

                dynamic mymodel = new ExpandoObject();
                mymodel.productImages = productImages;
                mymodel.ListPagination = this.GetProduct(currentPageIndex, SearchString, Products.Count());

                return View(nameof(Index), mymodel);
            }
            catch (System.Exception)
            {

                return null;
            }
        }


        [HttpPost("/tim-kiem-tin-tuc")]
        public IActionResult SearchWebPost(string SearchString)
        {
            try
            {
                List<WebPostViewModel> webPostViewModels = _context.WebPost.Where(item => item.PostTitle.ToUpper().Contains(SearchString.ToUpper())).ToList();
                
                return View(nameof(SearchWebPost), webPostViewModels);
            }
            catch (Exception)
            {
                return View(new List<WebPostViewModel>());
            }
        }

        public IActionResult SearchProduct()
        {
            List<CategoryViewModel> Category = _context.Category.Where(item => item.CategoryEnable == true).ToList();

            dynamic mymodel = new ExpandoObject();
            mymodel.Categorys = Category;

            return PartialView(nameof(SearchProduct), mymodel);
        }

        [Route("/tim-kiem-san-pham")]
        public IActionResult SearchProduct(string SearchPrice, int? currentPageIndex)
        {
            try
            {
                int currentIndex = 0;
                if (currentPageIndex == null)
                {
                    currentIndex = 1;
                }
                else
                {
                    currentIndex = (int)currentPageIndex;
                }

                if (string.IsNullOrEmpty(SearchPrice) == false)
                {
                    var StrSearch = SearchPrice.Split(' ');

                    decimal Search1 = 0;
                    decimal Search2 = 0;
                    for (int i = 0; i < StrSearch.Length; i++)
                    {
                        if (i == 0)
                        {
                            Search1 = Decimal.Parse(StrSearch[0]);
                        }
                        if (i == 3)
                        {
                            Search2 = Decimal.Parse(StrSearch[3]);
                        }
                    }
                    List<ProductViewModel> Products = _context.Product
                        .Where(item => item.SalePrice  > Search1)
                        .Where(item => item.SalePrice  < Search2)
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
                    mymodel.ListPagination = this.GetSearchProduct(SearchPrice,currentIndex, Products.Count());

                    return View(nameof(Index), mymodel);
                }

                return View();
            }
            catch (Exception)
            {
                return null;
            }
        }


        private PaginationModel GetProduct(int currentPage, string SearchString, int productCount)
        {
            int maxRows = 4;

            PaginationModel productList = new PaginationModel();
            productList.Products = (from customer in this._context.Product
                                    select customer)
                    .Where(item => item.ProductName.ToUpper().Contains(SearchString.ToUpper())).ToList()
                    .OrderByDescending(customer => customer.CreatedAt)
                    .Skip((currentPage - 1) * maxRows)
                    .Take(maxRows).ToList();

            double pageCount = (double)((decimal)productCount / Convert.ToDecimal(maxRows));
            productList.PageCount = (int)Math.Ceiling(pageCount);

            productList.CurrentPageIndex = currentPage;

            return productList;
        }
        private PaginationModel GetSearchProduct(string stringSearch, int currentPage, int productCount)
        {
            if (string.IsNullOrEmpty(stringSearch) == false)
            {
                var StrSearch = stringSearch.Split(' ');
                decimal Search1 = 0;
                decimal Search2 = 0;
                for (int i = 0; i < StrSearch.Length; i++)
                {
                    if (i == 0)
                    {
                        Search1 = Decimal.Parse(StrSearch[0]);
                    }
                    if (i == 3)
                    {
                        Search2 = Decimal.Parse(StrSearch[3]);
                    }
                }


                int maxRows = 4;

                PaginationModel productList = new PaginationModel();
                productList.Products = (from customer in this._context.Product
                                        select customer)
                        .Where(item => item.SalePrice > Search1)
                        .Where(item => item.SalePrice < Search2)
                        .ToList()
                        .OrderByDescending(customer => customer.CreatedAt)
                        .Skip((currentPage - 1) * maxRows)
                        .Take(maxRows).ToList();

                double pageCount = (double)((decimal)productCount / Convert.ToDecimal(maxRows));
                productList.PageCount = (int)Math.Ceiling(pageCount);

                productList.CurrentPageIndex = currentPage;

                return productList;
            }
            
            return null;
        }
    }
}
