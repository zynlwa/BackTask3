using BackendProject.App.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackendProject.App.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register(UserRegisterVm userRegisterVm)
        {
            return View();
        }
    }
}
