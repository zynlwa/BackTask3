﻿using BackendProject.App.Data;
using BackendProject.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BackendProject.App.Areas.Manage.Controllers;

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
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Genre genre)
    {
        if(!ModelState.IsValid)
            return View();
        if (pustokDbContext.Genres.Any(g => g.Name.ToLower() == genre.Name.ToLower()))
        {
            ModelState.AddModelError("Name", "This genre already exists!");
            return View();
        }
        pustokDbContext.Genres.Add(new Genre { Name = genre.Name });
        pustokDbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
    public IActionResult Delete(int id)
    {
        var genre = pustokDbContext.Genres.Find(id);
        if (genre == null) return NotFound();
        pustokDbContext.Genres.Remove(genre);
        pustokDbContext.SaveChanges();
        return Ok();
    }
    public IActionResult Edit(int id)
    {
        var genre = pustokDbContext.Genres.Find(id);
        if (genre == null) return NotFound();
        return View(genre);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Genre genre)
    {
        if (!ModelState.IsValid)
            return View();
        var existGenre = pustokDbContext.Genres.Find(genre.Id);
        if (existGenre == null) return NotFound();
        if (pustokDbContext.Genres.Any(g => g.Name.ToLower() == genre.Name.ToLower() && g.Id != genre.Id))
        {
            ModelState.AddModelError("Name", "This genre already exists!");
            return View();
        }
        existGenre.Name = genre.Name;
        pustokDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Detail(int id)
    {
        var genre = pustokDbContext.Genres
            .Include(g => g.Books) 
        .FirstOrDefault(g => g.Id == id);
        if (genre == null) return NotFound();

        return PartialView("_GenreDetailPartial", genre);
    }
}