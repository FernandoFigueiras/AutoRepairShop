using AutoRepairShop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.RepairViewModels
{
    public class StartRepairViewModel
    {


        public int VehicleId { get; set; }


        public Vehicle Vehicle { get; set; }


        public int ScheduleId { get; set; }


        public int ActiveScheduleId { get; set; }


        public ActiveSchedule Schedule { get; set; }



        public int DealershipId { get; set; }


        public Dealership Dealership { get; set; }

        [Required]
        [Display(Name ="Department")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a {0}")]
        public int DepartmentId { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; }


    }
}
