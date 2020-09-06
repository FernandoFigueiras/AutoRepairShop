using AutoRepairShop.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.Account
{
    public class UpdateUserDataViewModel : User
    {
        [Required]
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters long")]
        public new string FirstName { get; set; }


        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters long")]
        public new string LastName { get; set; }


        [Required]
        [MaxLength(150, ErrorMessage = "The field {0} only can contain {1} characters long")]
        public new string Address { get; set; }


        [Required]
        [Display(Name = "Zip Code")]
        [MaxLength(4, ErrorMessage = "The {0} first field must be {1} characters long")]
        public string ZipCode4 { get; set; }


        [Required]
        [Display(Name = "Zip Code")]
        [MaxLength(3, ErrorMessage = "The {0} second field must be {1} characters long")]
        public string ZipCode3 { get; set; }


        public string City { get; set; }


        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }


    }
}
