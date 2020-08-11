using System.ComponentModel.DataAnnotations;

namespace PortfolioWebApp.Models
{
    public class User
    {
        public int Id { get; set; }        
        [Display(Name = "Full Name"), Required(ErrorMessage = "Please enter name")]
        public string FullName { get; set; }
        [Display(Name = "Email"), Required(ErrorMessage = "Please enter Email"), DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Display(Name = "Password"), Required(ErrorMessage = "Please enter Password"), DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password"), DataType(DataType.Password), 
            Required(ErrorMessage = "Please enter Confirm Password"), Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}