using BackendProject.App.Models;
using BackendProject.App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackendProject.App.Controllers
{
    public class AccountController
        (UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager
        )
        : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVm userLoginVm,string ReturnUrl)
        {
            if(!ModelState.IsValid) return View(userLoginVm);
            
            var user = await userManager.FindByNameAsync(userLoginVm.UsernameOrEmail);
            if(user == null)
            {
                user = await userManager.FindByEmailAsync(userLoginVm.UsernameOrEmail);
                if(user == null)
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                    return View(userLoginVm);
                }
               
            }
            //var result = userManager.CheckPasswordAsync(user.Result, userLoginVm.Password);
            //if (!result.Result)
            //{
            //    ModelState.AddModelError("", "Username or password is incorrect");
            //    return View(userLoginVm);
            //}
            var result =await signInManager.PasswordSignInAsync(user, userLoginVm.Password, userLoginVm.RememberMe, true);
            if (result.IsLockedOut)
            {

                ModelState.AddModelError("", "Account lockedout");
                return View(userLoginVm);

            }
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(userLoginVm);
            }
            if (string.IsNullOrEmpty(ReturnUrl) || !Url.IsLocalUrl(ReturnUrl))
                return RedirectToAction("Index", "Home");

            return Redirect(ReturnUrl);
           
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Register(UserRegisterVm userRegisterVm)
        {
            if(!ModelState.IsValid)
            {
                return View(userRegisterVm);
            }
            var user = await userManager.FindByNameAsync(userRegisterVm.Username);
            if(user != null)
            {
                ModelState.AddModelError("Username", "This username is already taken");
                return View(userRegisterVm);
            }
            user = new AppUser
            {
                UserName = userRegisterVm.Username,
                Email = userRegisterVm.Email,
                FullName = userRegisterVm.FullName
            };
            var result = await userManager.CreateAsync(user, userRegisterVm.Password);
            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(userRegisterVm);
            }
            await userManager.AddToRoleAsync(user, "Member");
            return RedirectToAction("Login" , "Account");
        }
        //public async Task<IActionResult> CreateRole()
        //{
        //    if(!await roleManager.RoleExistsAsync("Admin"))
        //    {
        //      await roleManager.CreateAsync(new IdentityRole("Admin"));
        //    }
        //    if (!await roleManager.RoleExistsAsync("Member"))
        //    {
        //        await roleManager.CreateAsync(new IdentityRole("Member"));
        //    }
        //    return Content("Role created"); beyenilmir
        //}
        [Authorize(Roles ="Member")]
        public async Task<IActionResult> UserProfile()
        {
            UserProfileVm userProfileVm = new UserProfileVm();
            var user= await userManager.FindByNameAsync(User.Identity.Name);
            userProfileVm.UserUpdateProfileVm = new UserUpdateProfileVm
            {
                FullName = user.FullName,
                Email = user.Email,
                Username = user.UserName
            };


            return View(userProfileVm);
        }
        [HttpPost]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> UserProfile(UserProfileVm userProfileVm)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if(user == null)
            {
                return NotFound();
            }
            userProfileVm.UserUpdateProfileVm.Username = user.UserName;
            userProfileVm.UserUpdateProfileVm.Email = user.Email;
            userProfileVm.UserUpdateProfileVm.FullName = user.FullName;
            if(!ModelState.IsValid)
            {
                return View(userProfileVm);
            }
            var checkPassword = await userManager.CheckPasswordAsync(user, userProfileVm.UserUpdateProfileVm.CurrentPassword);
            if(!checkPassword)
            {
                ModelState.AddModelError("UserUpdateProfileVm.CurrentPassword", "Current password is incorrect");
                return View(userProfileVm);
            }
            if(user.UserName != userProfileVm.UserUpdateProfileVm.Username)
            {
                var existUser = await userManager.FindByNameAsync(userProfileVm.UserUpdateProfileVm.Username);
                if(existUser != null)
                {
                    ModelState.AddModelError("UserUpdateProfileVm.Username", "This username is already taken");
                    return View(userProfileVm);
                }
                user.UserName = userProfileVm.UserUpdateProfileVm.Username;
            }
            if(user.Email != userProfileVm.UserUpdateProfileVm.Email)
            {
                var existEmail = await userManager.FindByEmailAsync(userProfileVm.UserUpdateProfileVm.Email);
                if(existEmail != null)
                {
                    ModelState.AddModelError("UserUpdateProfileVm.Email", "This email is already taken");
                    return View(userProfileVm);
                }
                user.Email = userProfileVm.UserUpdateProfileVm.Email;
            }
            user.FullName = userProfileVm.UserUpdateProfileVm.FullName;
            var result = await userManager.ChangePasswordAsync(user, userProfileVm.UserUpdateProfileVm.CurrentPassword, userProfileVm.UserUpdateProfileVm.NewPassword);
            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(userProfileVm);
            }
            await userManager.UpdateAsync(user);
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
