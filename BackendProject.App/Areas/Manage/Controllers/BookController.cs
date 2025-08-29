using BackendProject.App.Data;
using BackendProject.App.Extensions;
using BackendProject.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.App.Areas.Manage.Controllers;
[Area("Manage")]
public class BookController
    (PustokDbContext pustokDbContext)
    : Controller
{
    public IActionResult Index()
    {
        var books =pustokDbContext.Books
            .Include(b=>b.Author)
            .Include(b=>b.Genre)
            .ToList();
        return View(books);
    }
    public IActionResult Delete(int id)
    {
        var book = pustokDbContext.Books
            .Include(b=>b.BookImages)
            .FirstOrDefault(b=>b.Id==id);
        if (book == null) return NotFound();
        pustokDbContext.Books.Remove(book);
        pustokDbContext.SaveChanges();
        FileManager.DeleteFile("image/products", book.MainImageUrl);
        FileManager.DeleteFile("image/products", book.HoverImageUrl);
        foreach (var image in book.BookImages)
        {
            FileManager.DeleteFile("image/products", image.ImageUrl);
        }

        return RedirectToAction("index");
    }
    public IActionResult Create()
    {
        ViewBag.Authors = pustokDbContext.Authors.ToList();
        ViewBag.Genres = pustokDbContext.Genres.ToList();
        ViewBag.Tags = pustokDbContext.Tags.ToList();   
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Book book)
    {
        ViewBag.Authors = pustokDbContext.Authors.ToList();
        ViewBag.Genres = pustokDbContext.Genres.ToList();

        if (!ModelState.IsValid) return View();
        if (book.MainPhoto == null)
        {
            ModelState.AddModelError("MainPhoto", "Main photo is required");
            return View();
        }
        if (book.HoverPhoto == null)
        {
            ModelState.AddModelError("HoverPhoto", "Hover photo is required");
            return View();
        }
        foreach(var tagId in book.TagIds)
        {
           if(!pustokDbContext.Tags.Any(t=>t.Id==tagId))
            {
                ModelState.AddModelError("TagIds", "Selected tag is not valid");
                return View();
            }
        }
        foreach (var tagId in book.TagIds)
        {
            var bookTag = new BookTag
            {
                TagId = tagId,
                BookId = book.Id
            };
            book.BookTags.Add(bookTag);
        }
        book.MainImageUrl = book.MainPhoto.SaveFile("image/products");
        book.HoverImageUrl = book.HoverPhoto.SaveFile("image/products");
        if (book.Photos != null)
        {
           
            foreach (var photo in book.Photos)
            {
                
                var bookImage = new Models.BookImage
                {
                   ImageUrl = photo.SaveFile("image/products")

                };
                book.BookImages.Add(bookImage);
            }
        }
        pustokDbContext.Books.Add(book);
        pustokDbContext.SaveChanges();
        return RedirectToAction("index");
    }
    public IActionResult Edit(int id)
    {
        var book = pustokDbContext.Books
            .Include(b=>b.BookTags)
            .FirstOrDefault(b=>b.Id==id);
        if (book == null) return NotFound();
        ViewBag.Authors = pustokDbContext.Authors.ToList();
        ViewBag.Genres = pustokDbContext.Genres.ToList();
        ViewBag.Tags = pustokDbContext.Tags.ToList();
        book.TagIds = book.BookTags.Select(bt => bt.TagId).ToList();
        return View(book);
    }
}
