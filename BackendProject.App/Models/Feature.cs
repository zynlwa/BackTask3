using BackendProject.App.Models.Common;

namespace BackendProject.App.Models
{
    public class Feature:BaseEntity
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
