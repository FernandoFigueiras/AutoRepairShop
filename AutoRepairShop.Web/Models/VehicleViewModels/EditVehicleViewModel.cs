using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.VehicleViewModels
{
    public class EditVehicleViewModel : AddVehicleViewModel
    {

        public string BrandName { get; set; }



        public string Fueltype { get; set; }



        public string ColorName { get; set; }

    }
}
