namespace BackendProject.App.Models.Common
{
    public class AuditEntity:BaseEntity
    {
        public DateTime CreatAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
