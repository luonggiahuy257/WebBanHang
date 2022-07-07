using Microsoft.AspNetCore.Mvc;

namespace BanHangOnline.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BlogList()
        {
            return View();
        }
        public IActionResult BlogDetail()
        {
            return View();
        }
    }
}
