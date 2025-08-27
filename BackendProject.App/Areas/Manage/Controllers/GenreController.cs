using BackendProject.App.Data;
using Microsoft.AspNetCore.Mvc;

namespace BackendProject.App.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class GenreController
         (PustokDbContext pustokDbContext)
        : Controller
     
    {
        public IActionResult Index()
        {
            var genres = pustokDbContext.Genres.ToList();
            return View(genres);
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
