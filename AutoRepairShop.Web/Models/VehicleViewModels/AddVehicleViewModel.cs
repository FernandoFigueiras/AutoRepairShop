using AutoRepairShop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AutoRepairShop.Web.Models.VehicleViewModels
{
    public class AddVehicleViewModel : Vehicle
    {

        

        public IEnumerable<SelectListItem> Brands { get; set; }



        public IEnumerable<SelectListItem> Models { get; set; }



        public string ModelName { get; set; }



        public IEnumerable<SelectListItem> Fuels { get; set; }




        public IEnumerable<SelectListItem> Colors { get; set; }

    }
}
