using AutoRepairShop.Web.Data.Entities;

namespace AutoRepairShop.Web.Models.VehicleViewModels
{
    public class VehicleDetailsViewModel : Vehicle
    {

        public string BrandName { get; set; }



        public string ModelName { get; set; }



        public string Fueltype { get; set; }



        public string ColorName { get; set; }
    }
}
