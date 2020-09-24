using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.ActiveScheduleViewModel
{
    public class InitScheduleByDealership
    {
        public int DealershipId { get; set; }


        [Required]
        public int ServiceId { get; set; }


        public IEnumerable<SelectListItem> Services { get; set; }


        [Display(Name = "User Name")]
        [EmailAddress]
        public string UserName { get; set; }



    }
}
