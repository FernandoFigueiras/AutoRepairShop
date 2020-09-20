using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.ActiveScheduleViewModel
{
    public class BeginScheduleViewModel
    {
        [Required]
        [Display(Name = "Vehicle")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a vehicle")]
        public int VehicleId { get; set; }



        public IEnumerable<SelectListItem> Vehicles { get; set; }


        [Required]
        [Display(Name = "Services")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a sevice")]
        public int ServicesSuppliedId { get; set; }



        public IEnumerable<SelectListItem> ServicesSupplied { get; set; }

        [Required]
        [Display(Name = "Dealership")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a dealership")]
        public int DealershipId { get; set; }



        public IEnumerable<SelectListItem> Dealerships { get; set; }



    }
}
