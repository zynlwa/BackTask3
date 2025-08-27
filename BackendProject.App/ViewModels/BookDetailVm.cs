using BackendProject.App.Models;

namespace BackendProject.App.ViewModels
{
    public class BookDetailVm
    {
        public Book Book { get; set; }
        public List<Book> RelatedBooks { get; set; }

    }
}
