using AutoRepairShop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.ActiveScheduleViewModel
{
    public class ActiveScheduleViewModel
    {

        public int ActiveAcheduleId { get; set; }


        [Display(Name = "Licence Plate")]
        public string LicencePlate { get; set; }



        public DateTime Date { get; set; }


       


        public string Dealership { get; set; }




        public string Service { get; set; }

    }
}
