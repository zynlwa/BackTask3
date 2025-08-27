using System.ComponentModel.DataAnnotations;

namespace BackendProject.App.Models
{
    public class Setting
    {

        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
        //public string Email { get; set; }
        //public string SupportEmail { get; set; }
        //public string PhoneNumber { get; set; }
        //public string SupportPhoneNumber { get; set; }
        //public string Address { get; set; }
        //public string LogoUrl { get; set; }
    }
}
