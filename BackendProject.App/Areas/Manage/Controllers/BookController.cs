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
        var books = pustokDbContext.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .ToList();
        return View(books);
    }
    public IActionResult Delete(int id)
    {
        var book = pustokDbContext.Books
            .Include(b => b.BookImages)
            .FirstOrDefault(b => b.Id == id);
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
        foreach (var tagId in book.TagIds)
        {
            if (!pustokDbContext.Tags.Any(t => t.Id == tagId))
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
            .Include(b => b.BookTags)
            .Include(b => b.BookImages)
            .FirstOrDefault(b => b.Id == id);
        if (book == null) return NotFound();
        ViewBag.Authors = pustokDbContext.Authors.ToList();
        ViewBag.Genres = pustokDbContext.Genres.ToList();
        ViewBag.Tags = pustokDbContext.Tags.ToList();
        book.TagIds = book.BookTags.Select(bt => bt.TagId).ToList();
        return View(book);
    }
    public IActionResult DeleteImage(int id)
    {
        var image = pustokDbContext.BookImages.FirstOrDefault(bi => bi.Id == id);
        if (image == null) return NotFound();
        FileManager.DeleteFile("image/products", image.ImageUrl);
        pustokDbContext.BookImages.Remove(image);
        pustokDbContext.SaveChanges();
        return RedirectToAction("Edit", new { id = image.BookId });
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Book book)
    {
        ViewBag.Authors = pustokDbContext.Authors.ToList();
        ViewBag.Genres = pustokDbContext.Genres.ToList();
        ViewBag.Tags = pustokDbContext.Tags.ToList();
        var existBook = pustokDbContext.Books
            .Include(b => b.BookTags)
            .Include(b => b.BookImages)
            .FirstOrDefault(b => b.Id == book.Id);
        if (existBook == null) return NotFound();
        if (!ModelState.IsValid) return View(existBook);
        if(!pustokDbContext.Authors.Any(a=>a.Id==book.AuthorId))
        {
            ModelState.AddModelError("AuthorId", "Selected author is not valid");
            return View(existBook);
        }
        if (!pustokDbContext.Genres.Any(g => g.Id == book.GenreId))
        {
            ModelState.AddModelError("GenreId", "Selected genre is not valid");
            return View(existBook);
        }
        foreach (var tagId in book.TagIds)
        {
            if (!pustokDbContext.Tags.Any(t => t.Id == tagId))
            {
                ModelState.AddModelError("TagIds", "Selected tag is not valid");
                return View(existBook);
            }
        }
        existBook.BookTags.RemoveAll(bt => !book.TagIds.Contains(bt.TagId));
        var existingTagIds = existBook.BookTags.Select(bt => bt.TagId).ToList();
        var newTagIds = book.TagIds.Except(existingTagIds).ToList();
        foreach (var tagId in newTagIds)
        {
            var bookTag = new BookTag
            {
                TagId = tagId,
                BookId = book.Id
            };
            existBook.BookTags.Add(bookTag);
        }
        if (book.MainPhoto != null)
        {
            FileManager.DeleteFile("image/products", existBook.MainImageUrl);
            existBook.MainImageUrl = book.MainPhoto.SaveFile("image/products");
        }
        if (book.HoverPhoto != null)
        {
            FileManager.DeleteFile("image/products", existBook.HoverImageUrl);
            existBook.HoverImageUrl = book.HoverPhoto.SaveFile("image/products");
        }
        if (book.Photos != null)
        {
            foreach (var photo in book.Photos)
            {
                var bookImage = new Models.BookImage
                {
                    ImageUrl = photo.SaveFile("image/products")
                };
                existBook.BookImages.Add(bookImage);
            }
        }
        existBook.Title = book.Title;
        existBook.Description = book.Description;
        existBook.Price = book.Price;
        existBook.DiscountPercentage = book.DiscountPercentage;
        existBook.IsFeatured = book.IsFeatured;
        existBook.IsNew = book.IsNew;
        existBook.InStock = book.InStock;
        existBook.Code = book.Code;
        existBook.AuthorId = book.AuthorId;
        existBook.GenreId = book.GenreId;
        pustokDbContext.SaveChanges();
        return RedirectToAction("index");

    }
}