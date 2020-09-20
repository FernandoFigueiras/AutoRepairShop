using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.ActiveScheduleViewModel
{
    public class EditScheduleViewModel
    {
        public int ActiveScheduleId { get; set; }

        [Display(Name ="Licence Plate")]
        public string LicencePlate { get; set; }


        public string Dealership { get; set; }

        [Display(Name ="Services")]
        public int ServicesSupliedId { get; set; }


        public IEnumerable<SelectListItem> Services { get; set; }

        [Required]
        public DateTime Day { get; set; }


        public string Remarks { get; set; }


        public string DaysToDisable { get; set; }
    }
}
