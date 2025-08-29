using BackendProject.App.Attributes;
using BackendProject.App.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendProject.App.Models
{
    public class Book:AuditEntity
    {
        [Required]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Desc cannot be longer than 100 characters.")]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercentage { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsNew { get; set; }

        public bool InStock { get; set; }

        public string Code { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public List<BookImage> BookImages { get; set; }
  
        public string MainImageUrl { get; set; }
       
        public string HoverImageUrl { get; set; }
        public List<BookTag> BookTags { get; set; }
        [NotMapped]
        [FileLength(2)]
        [FileType("image/jpeg","image/png","image/jpg")]
        public IFormFile MainPhoto { get; set; }
        [NotMapped]
        [FileLength(2)]
        [FileType("image/jpeg", "image/png", "image/jpg")]
        public IFormFile HoverPhoto { get; set; }
        [NotMapped]
        [FileLength(2)]
        [FileType("image/jpeg", "image/png", "image/jpg")]
        public IFormFile[] Photos { get; set; }
        [NotMapped]
        public List<int> TagIds { get; set; }

        public Book()
        {
            BookImages = new List<BookImage>();
            BookTags = new List<BookTag>();
            TagIds = new List<int>();
        }
    }
   
}
