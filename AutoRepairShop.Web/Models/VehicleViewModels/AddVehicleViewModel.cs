using AutoRepairShop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.VehicleViewModels
{
    public class AddVehicleViewModel: Vehicle
    {


        public IEnumerable<SelectListItem> Brands { get; set; }



        public IEnumerable<SelectListItem> Models { get; set; }




        [Display(Name ="Model")]
        public string ModelName { get; set; }




        public IEnumerable<SelectListItem> Fuels { get; set; }




        public IEnumerable<SelectListItem> Colors { get; set; }

    }
}
