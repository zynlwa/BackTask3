using BackendProject.App.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace BackendProject.App.Models
{
    public class Author:BaseEntity
    {
        [Required]
        [StringLength(20, ErrorMessage = "Title cannot be longer than 50 characters.")]
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }

}
