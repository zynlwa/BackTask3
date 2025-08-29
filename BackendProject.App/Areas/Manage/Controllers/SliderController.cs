using BackendProject.App.Data;
using BackendProject.App.Extensions;
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
        var file = slider.File;
        if (file == null)
        {
            ModelState.AddModelError("File", "File is required"); return View();
        }
        if (!file.CheckType("image/"))
        {
            ModelState.AddModelError("File", "File type must be image"); return View();
        }
        //if (!file.CheckSize(2))
        //{
        //    ModelState.AddModelError("File", "File size must be max 2MB"); return View();
        //}

        slider.ImageUrl = file.SaveFile("image/bg-images");
        slider.CreatAt= DateTime.Now;
        pustokDbContext.Sliders.Add(slider);
        pustokDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Delete(int id)
    {
        var slider = pustokDbContext.Sliders.Find(id);
        if (slider == null) return NotFound();
        pustokDbContext.Sliders.Remove(slider);
        pustokDbContext.SaveChanges();
        FileManager.DeleteFile("image/bg-images", slider.ImageUrl);

        return Ok();
    }
    public IActionResult Edit(int id)
    {
        var slider = pustokDbContext.Sliders.Find(id);
        if (slider == null) return NotFound();
        return View(slider);
    }
    [HttpPost]
    public IActionResult Edit(Slider slider)
    {
        if (!ModelState.IsValid)
            return View();
        var existSlider = pustokDbContext.Sliders.Find(slider.Id);
        if (existSlider == null) return NotFound();
        var file = slider.File;
        if (file != null)
        {
            if (!file.CheckType("image/"))
            {
                ModelState.AddModelError("File", "File type must be image"); return View();
            }
            if (!file.CheckSize(2))
            {
                ModelState.AddModelError("File", "File size must be max 2MB"); return View();
            }
            FileManager.DeleteFile("image/bg-images", existSlider.ImageUrl);
            existSlider.ImageUrl = file.SaveFile("image/bg-images");
        }
        existSlider.Title = slider.Title;
        existSlider.Description = slider.Description;
        existSlider.ButtonText = slider.ButtonText;
        existSlider.ButtonLink = slider.ButtonLink;
        existSlider.Order = slider.Order;
        existSlider.UpdateAt= DateTime.Now;
        pustokDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
