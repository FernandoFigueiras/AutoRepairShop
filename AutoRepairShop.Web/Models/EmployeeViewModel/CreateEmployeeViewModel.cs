using AutoRepairShop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Crypto.Tls;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.EmployeeViewModel
{
    public class CreateEmployeeViewModel
    {

        public IEnumerable<SelectListItem> Dealerships { get; set; }



        public IEnumerable<SelectListItem> Departments { get; set; }




        [Required]
        [EmailAddress]
        public string UserName { get; set; }



        public string Password { get => "P@ssw0rd"; }




        public User User { get; set; }




        [Required]
        [Display(Name ="Dealership")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a {0}")]
        public int DealershipId { get; set; }


        [Required]
        [Display(Name = "Department")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a {0}")]
        public int DepartmentId { get; set; }


        [Required]
        [Display(Name = "Position")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a {0}")]
        public int PositionId { get; set; }


    }
}
