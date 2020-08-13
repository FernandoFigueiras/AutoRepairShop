using AutoRepairShop.Web.Data.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.Account
{
    public class ChangePasswordViewModel : User
    {
        [Required]
        [PasswordPropertyText]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }


        [Required]
        [PasswordPropertyText]
        [Display(Name = "New Password")]
        [MinLength(6, ErrorMessage = "The {0} must contain at least {1} characters")]
        public string NewPassword { get; set; }




        [PasswordPropertyText]
        [Compare("NewPassword")]
        [Display(Name = "Confirm New Password")]
        public string ConfirmPassword { get; set; }


    }
}
