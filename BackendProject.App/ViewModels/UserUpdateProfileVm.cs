using System.ComponentModel.DataAnnotations;

namespace BackendProject.App.ViewModels
{
    public class UserUpdateProfileVm
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }

    }
}
