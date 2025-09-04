using System.ComponentModel.DataAnnotations;

namespace BackendProject.App.ViewModels
{
    public class UserRegisterVm
    {
        [Required]
        public string Username { get; set; }
        [Required,MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MinLength(6)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password do not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }

    }
}
