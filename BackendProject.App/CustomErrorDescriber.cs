using Microsoft.AspNetCore.Identity;

namespace BackendProject.App
{
    public class CustomErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresNonAlphanumeric()

        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "Password must contain at least one special character (!, @, #, $, %, ^, &, *)"
            };
        }
       
    }
}
