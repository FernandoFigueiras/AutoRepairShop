using AutoRepairShop.Web.Data.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.Account
{
    public class RegisterViewModel : User
    {

        [Required]
        [EmailAddress]
        [Display(Name ="User Name")]
        public new string UserName { get; set; }



        [Required]
        [MinLength(6, ErrorMessage = "The field {0} must contain at least {1} characters long")]
        [PasswordPropertyText]
        public string Password { get; set; }


        [Required]
        [PasswordPropertyText]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
