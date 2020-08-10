using AutoRepairShop.Web.Data.Entities;

namespace AutoRepairShop.Web.Models
{
    public class DeleteVehicleViewModel : Vehicle
    {

        public string Brand { get; set; }


        public string ModelName { get; set; }


        public string FuelType { get; set; }
    }
}
