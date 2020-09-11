using AutoRepairShop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.ActiveScheduleViewModel
{
    public class NewScheduleViewModel
    {

        public int VehicleId { get; set; }


        [Display(Name ="Services")]
        public IEnumerable<ServicesSupplied> ServicesSupplied { get; set; }


        public IEnumerable<Dealership> Dealerships { get; set; }


        public DateTime Day { get; set; }


        public string Remarks { get; set; }


        public double Mileage { get; set; }


    }
}
