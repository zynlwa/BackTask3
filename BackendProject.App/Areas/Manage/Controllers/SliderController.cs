using BackendProject.App.Data;
using BackendProject.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendProject.App.Areas.Manage.Controllers;
[Area("Manage")]

public class SliderController 
    (PustokDbContext pustokDbContext)
    : Controller
{
    public IActionResult Index()
    {
        var sliders = pustokDbContext.Sliders.ToList();
        return View(sliders);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]

    public IActionResult Create(Slider slider)
    {
        if (!ModelState.IsValid)
            return View();
        pustokDbContext.Sliders.Add(slider);
        pustokDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
