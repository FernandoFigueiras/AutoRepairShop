using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.ActiveScheduleViewModel
{
    public class InitScheduleByDealershipNoUser
    {

        public string UserID { get; set; }



        public int DealershipId { get; set; }


        [Required]
        public int ServiceId { get; set; }


        public IEnumerable<SelectListItem> Services { get; set; }


        [Required]
        [Display(Name = "Licence Plate")]
        public string Licenceplate { get; set; }
    }
}
