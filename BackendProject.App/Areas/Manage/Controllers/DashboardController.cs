using Microsoft.AspNetCore.Mvc;

namespace BackendProject.App.Areas.Manage.Controllers;
   [Area("Manage")]

    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

