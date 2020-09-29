using AutoRepairShop.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.VehicleViewModels
{
    public class VehicleDetailsViewModel : Vehicle
    {

        [Display(Name = "Brand")]
        public string BrandName { get; set; }




        [Display(Name = "Model")]
        public string ModelName { get; set; }




        [Display(Name = "Fuel")]
        public string Fueltype { get; set; }




        [Display(Name = "Color")]
        public string ColorName { get; set; }
    }
}
