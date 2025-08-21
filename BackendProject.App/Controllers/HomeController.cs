using BackendProject.App.Data;
using BackendProject.App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                Sliders = pustokDbContext.Sliders.ToList(),
                FeaturedBooks=pustokDbContext.Books
                .Include(b=>b.Author)
                .Where(b=>b.IsFeatured).ToList(),

                NewBooks=pustokDbContext.Books
                .Include(b=> b.Author)
                .Where(b=>b.IsNew).ToList(),

                DiscountBooks=pustokDbContext.Books
                .Include (b=>b.Author)  
                .Where(b=>b.DiscountPercentage> 0).ToList(),
                Features=pustokDbContext.Features.ToList()


            };
            return View(homeVm);
        }

     
    }
}
