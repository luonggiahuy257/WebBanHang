using BanHangOnline.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BanHangOnline.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email

                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //add role here
                    //await _userManager.AddToRoleAsync(user, "Admin");
                    Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(user, "Member");
                    newUserRole.Wait();
                    return RedirectToAction("RegisterSuccess", "Account");
                }
            }
            ModelState.AddModelError("", "Invalid Register.");
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Email);
                    //user role list here
                    var roles = await _userManager.GetRolesAsync(user);
                    //get default role here
                    string role = roles.FirstOrDefault();
                    if (role.Equals("Admin"))
                    {
                        return RedirectToAction("Home", "Managers");
                    }
                    else if (role.Equals("Member"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //do something here. put in your logic 
                    }
                }
            }
            ModelState.AddModelError("", "Invalid ID or Password");
            return View(model);
        }

        public ActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            //FormsAuthentication.SignOut();
            //Session.Clear();
            //Session.RemoveAll();
            //Session.Abandon();
            return RedirectToAction("Privacy", "Home");
        }

        public ActionResult RegisterSuccess()
        {
            return View();
        }

        public ActionResult LoginSuccess()
        {
            return View();
        }
    }
}
