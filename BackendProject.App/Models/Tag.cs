using BackendProject.App.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace BackendProject.App.Models
{
    public class Tag:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public List<BookTag> BookTags { get; set; }
    }
}
