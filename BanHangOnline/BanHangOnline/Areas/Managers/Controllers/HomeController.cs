using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BanHangOnline.Areas.Manager.Controllers
{
    [Area("Managers")]
    //[Authorize(Roles = ("Admin"))]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Abount()
        {
            return View();
        }
    }
}
