using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.Account
{
    public class LoginViewModel
    {



        [Required]
        [EmailAddress]
        [Display(Name ="User Name")]
        public string UserName { get; set; }


        [Required]
        [MinLength(6)]
        public string Password { get; set; }


        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }


    }
}
