using BanHangOnline.Database;
using BanHangOnline.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BanHangOnline.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly DataContext _context;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
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
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var user = new IdentityUser
                        {
                            UserName = model.Email,
                            Email = model.Email

                        };

                        if (model.GenderId == 0)
                        {
                            model.GenderId = 999;
                        }
                        DateTime now = DateTime.Now;
                        UserProfileViewModel userProfile = new UserProfileViewModel
                        {
                            NetId = user.Id,
                            LastName = model.LastName,
                            FirstName = model.FirstName,
                            //DescriptionUser =model.DescriptionUser,
                            AddressInfo = model.Address,
                            Email = model.Email,
                            PhoneNumber = model.PhoneNumber,
                            //Picture = model.Picture,
                            GenderId = model.GenderId,
                            CreatedAt = now,
                            CreatedBy = user.Id,
                            UpdatedAt = now,
                            UpdatedBy = user.Id,
                        };

                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            //add role here
                            Task<IdentityResult> newUserRole = _userManager.AddToRoleAsync(user, "Member");
                            newUserRole.Wait();

                            _context.Add(userProfile);
                            await _context.SaveChangesAsync();
                            transaction.Commit();

                            return RedirectToAction("RegisterSuccess", "Account");
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Đăng ký không hợp lệ");
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
                    var role = roles.FirstOrDefault();
                    if (role != null)
                    {
                        if (role.Equals("Admin"))
                        {
                            return RedirectToAction("Home", "Manager");
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
            }
            ModelState.AddModelError("", "Sai gmail hoặc Password");
            return View(model);
        }

        public ActionResult LogOut()
        {
            var session = HttpContext.Session;
            _signInManager.SignOutAsync();
            session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RegisterSuccess()
        {
            return View();
        }

        public ActionResult LoginSuccess()
        {
            return View();
        }

        //public async Task<IdentityResult> ChangePasswordAsync(TKey userId, string newPassword)
        //{
        //    var store = this.Store as IUserPasswordStore;
        //    if (store == null)
        //    {
        //        var errors = new string[]
        //        {
        //        "Current UserStore doesn't implement IUserPasswordStore"
        //        };

        //        return Task.FromResult<IdentityResult>(new IdentityResult(errors) { Succeeded = false });
        //    }

        //    if (PasswordValidator != null)
        //    {
        //        var passwordResult = await PasswordValidator.ValidateAsync(password);
        //        if (!password.Result.Success)
        //            return passwordResult;
        //    }

        //    var newPasswordHash = this.PasswordHasher.HashPassword(newPassword);

        //    await store.SetPasswordHashAsync(userId, newPasswordHash);
        //    return Task.FromResult<IdentityResult>(IdentityResult.Success);
        //}

        //public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string newPassword)
        //{
        //    if (!SupportsUserPassword)
        //    {
        //        return IdentityResult.Failed(new IdentityError
        //        {
        //            Description = "Current UserStore doesn't implement IUserPasswordStore"
        //        });
        //    }

        //    var result = await UpdatePasswordHash(user, newPassword, true);
        //    if (result.Succeeded)
        //        await UpdateAsync(user);

        //    return result;
        //}
    }
}
