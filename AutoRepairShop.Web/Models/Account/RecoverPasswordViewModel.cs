using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.Account
{
    public class RecoverPasswordViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
