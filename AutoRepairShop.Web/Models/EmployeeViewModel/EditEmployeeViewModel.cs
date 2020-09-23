using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.EmployeeViewModel
{
    public class EditEmployeeViewModel
    {

        public int EmployeeId { get; set; }


        public string UserId { get; set; }


        public string OldRole { get; set; }




        public bool IsActive { get; set; }



        public IEnumerable<SelectListItem> Dealerships { get; set; }



        public IEnumerable<SelectListItem> Departments { get; set; }



        
        [Display(Name = "Dealership")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a {0}")]
        public int DealershipId { get; set; }




        
        [Display(Name = "Department")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a {0}")]
        public int DepartmentId { get; set; }


    }
}
