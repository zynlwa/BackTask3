using Microsoft.AspNetCore.Identity;

namespace BackendProject.App.Models
{
    public class AppUser: IdentityUser
    {
        public string FullName { get; set; }

    }
}
