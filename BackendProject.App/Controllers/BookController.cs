using BackendProject.App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.App.Controllers
{
    public class BookController
        (PustokDbContext pustokDbContext)
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
                .FirstOrDefault(b=>b.Id == id);
            if(book == null)
                return NotFound();
            return View(book);
            
        }
    }
}
