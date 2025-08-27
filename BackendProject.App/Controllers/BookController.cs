using AutoMapper;
using BackendProject.App.Data;
using BackendProject.App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.App.Controllers
{
    public class BookController
        (PustokDbContext pustokDbContext,
        IMapper mapper
        )
        : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if(id== null)
                return NotFound();
            var book =pustokDbContext.Books
                .Include(b=>b.BookImages)
                .Include(b=>b.Author)
                .Include(b=>b.Genre)
                .Include(b=>b.BookTags)
                .ThenInclude(b=>b.Tag)
                .FirstOrDefault(b=>b.Id == id);
            if(book == null)
                return NotFound();
            BookDetailVm bookDetailVm = new BookDetailVm
            {
                Book = book,
                RelatedBooks = pustokDbContext.Books
                .Where(b => b.GenreId == book.GenreId && b.Id != book.Id)
                .Include(b => b.BookImages)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.BookTags)
                .ThenInclude(b => b.Tag)
                .ToList()

            };
            return View(bookDetailVm);
            
        }
        public IActionResult BookModal(int? id)
        {
            if (id == null)
                return NotFound();
            var book = pustokDbContext.Books
                .Include(b => b.BookImages)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.BookTags)
                .ThenInclude(b => b.Tag)
                .FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();
            return PartialView("_BookModal", book);

        }
        public IActionResult Test(int? id)
        {
            if (id == null)
                return NotFound();
            var book = pustokDbContext.Books
                .Where(b=>b.Id == id) //eger booktestvmde id olmasa bu sekilde de yazila biler ve firstordefaultun ici bos qalir
                .Include(b => b.BookImages)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.BookTags)
                .ThenInclude(b => b.Tag)
                //.Select(b=>new BookTestVm
                //{
                    
                //    Title=b.Title,
                //    Description=b.Description,
                //    IsFeatured=b.IsFeatured,
                //    IsNew=b.IsNew,
                //    InStock=b.InStock,
                //    AuthorName =b.Author.Name,
                //    GenreName=b.Genre.Name,
                //    Price=b.Price,
                //    DiscountPercentage=b.DiscountPercentage,
                //    Code=b.Code,
                //    MainImageUrl =b.MainImageUrl,
                //    HoverImageUrl=b.HoverImageUrl,
                //    BookImageUrls =b.BookImages.Select(bi=>bi.ImageUrl).ToList(),
                //    TagNames=b.BookTags.Select(bt=>bt.Tag.Name).ToList()
                //})
                .FirstOrDefault();
            if (book == null)
                return NotFound();
            BookTestVm bookTestVm= mapper.Map<BookTestVm>(book);
            return Ok(bookTestVm);

        }
    }
}
