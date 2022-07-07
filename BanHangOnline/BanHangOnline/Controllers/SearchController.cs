using BanHangOnline.Database;
using BanHangOnline.Models;
using BanHangOnline.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public IActionResult Index(string SearchString)
        {
            try
            {
                if (string.IsNullOrEmpty(SearchString))
                {
                    return View();
                }
                List<SearchProduct> modelSearch = new List<SearchProduct>();

                List<ProductViewModel> Product = _context.Product.Where(item => item.ProductName.ToUpper().Contains(SearchString.ToUpper())).ToList();

                foreach (ProductViewModel product in Product)
                {
                    ProductImageViewModel images = _context.ProductImage.Where(item => item.ProductId == product.Id).First();

                    SearchProduct searchProduct = new SearchProduct();
                    searchProduct.ProductImageSearch = images;
                    searchProduct.ProductSearch = product;
                    modelSearch.Add(searchProduct);
                }

                return View(modelSearch);
            }
            catch (System.Exception)
            {

                return null;
            }
        }
    }
}
