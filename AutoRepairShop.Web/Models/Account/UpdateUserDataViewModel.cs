﻿using AutoRepairShop.Web.Data.Entities;
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
        public string ZipCode4 { get; set; }


        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode3 { get; set; }


        public int CountyId { get; set; }


        public string County { get; set; }


    }
}
