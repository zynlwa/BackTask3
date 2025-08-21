using System.Diagnostics;
using BackendProject.App.Data;
using BackendProject.App.Models;
using BackendProject.App.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BackendProject.App.Controllers
{
    public class HomeController 
        (PustokDbContext pustokDbContext)
        : Controller
    {
     

        public IActionResult Index()
        {
            HomeVm homeVm = new HomeVm()
            {
                Sliders = pustokDbContext.Sliders.ToList()
            };
            return View(homeVm);
        }

     
    }
}
