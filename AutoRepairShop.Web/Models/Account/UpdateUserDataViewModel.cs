using AutoRepairShop.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.Account
{
    public class UpdateUserDataViewModel
    {

        public User User { get; set; }

        public IEnumerable<Vehicle> Vehicles { get; set; }

        public Vehicle vehicle { get; set; }


        [Required]
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters long")]
        public string FirstName { get; set; }



        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters long")]
        public string LastName { get; set; }



        [Required]
        [MaxLength(150, ErrorMessage = "The field {0} only can contain {1} characters long")]
        public string Address { get; set; }



        [Required]
        [Display(Name = "Zip Code")]
        [MaxLength(4, ErrorMessage = "The {0} first field must be {1} characters long")]
        public string ZipCode4 { get; set; }


        [Required]
        [Display(Name = "Zip Code")]
        [MaxLength(3, ErrorMessage = "The {0} second field must be {1} characters long")]
        public string ZipCode3 { get; set; }


        public string City { get; set; }

        [Required]
        [Display(Name ="Tax Payer Number")]
        [MaxLength(15, ErrorMessage = "The {0} second field must be {1} characters long")]
        public string TaxPayerNumber { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [MaxLength(15, ErrorMessage = "The {0} second field must be {1} characters long")]
        public string PhoneNumber { get; set; }



        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }


        public string OldPassword { get; set; }


        public string NewPassword { get; set; }


        public string ConfirmPassword { get; set; }

    }
}
