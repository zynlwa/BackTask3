using BackendProject.App.Areas.Manage.ViewModels;
using BackendProject.App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendProject.App.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController
        (UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
        : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVm adminLoginVm)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Username or Password is incorrect");
                return View();
            }
            var user = await userManager.FindByNameAsync(adminLoginVm.Username);
            if(user==null)
            {
                ModelState.AddModelError("", "Username or Password is incorrect");
                return View();
            }
            var result = await signInManager.PasswordSignInAsync(adminLoginVm.Username, adminLoginVm.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password is incorrect");
                return View();
            }

            await signInManager.SignInAsync(user, true);



            return RedirectToAction("Index","Dashboard");
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> CreateAdmin()
        {
            var appUser = new AppUser
            {
                FullName = "_Admin",
                UserName = "Admin",
                Email="admin@gmail.com"
            };
            var result = await userManager.CreateAsync(appUser, "_Admin123");
            return Json(result);
        }
        public async Task<IActionResult> UserProfile()
        {
            //var user = await userManager.FindByNameAsync(User.Identity.Name);
            var userId=User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier)?.Value;
            return Json(userId);
        }

    }
}
