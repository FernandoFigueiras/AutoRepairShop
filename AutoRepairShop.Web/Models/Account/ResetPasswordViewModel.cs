using AutoRepairShop.Web.Data.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.Account
{
    public class ResetPasswordViewModel : User
    {

        [Required]
        [PasswordPropertyText]
        [Display(Name = "Password")]
        [MinLength(6, ErrorMessage = "The {0} must contain at least {1} characters")]
        public string NewPassword { get; set; }




        [PasswordPropertyText]
        [Compare("NewPassword")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
