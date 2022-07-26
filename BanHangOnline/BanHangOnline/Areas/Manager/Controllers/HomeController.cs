using BanHangOnline.Models;
using BanHangOnline.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BanHangOnline.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = ("Admin"))]
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
        public ActionResult ContactSuccess()
        {
            return View();
        }
    }
}
