using System;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.ActiveScheduleViewModel
{
    public class DeleteScheduleViewModel
    {

        public int ActiveScheduleId { get; set; }



        [Display(Name = "Licence Plate")]
        public string LicencePLate { get; set; }



        public string Dealership { get; set; }




        public string Service { get; set; }



        [Display(Name = "Schedule Day")]
        public DateTime ScheduleDay { get; set; }


    }
}
