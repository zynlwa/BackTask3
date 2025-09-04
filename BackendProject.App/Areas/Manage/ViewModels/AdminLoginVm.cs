using System.ComponentModel.DataAnnotations;

namespace BackendProject.App.Areas.Manage.ViewModels
{
    public class AdminLoginVm
    {
        [Required]
        public string Username { get; set; }

        [Required,MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
