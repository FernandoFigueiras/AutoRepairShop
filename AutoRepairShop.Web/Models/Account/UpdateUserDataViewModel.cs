using AutoRepairShop.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.Account
{
    public class UpdateUserDataViewModel
    {

        public User User { get; set; }


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
