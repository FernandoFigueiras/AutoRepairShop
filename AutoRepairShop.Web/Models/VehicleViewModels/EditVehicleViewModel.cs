using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.VehicleViewModels
{
    public class EditVehicleViewModel : AddVehicleViewModel
    {

        [Display(Name ="Brand")]
        public string BrandName { get; set; }




        [Display(Name ="Fuel")]
        public string Fueltype { get; set; }





        [Display(Name ="Color")]
        public string ColorName { get; set; }

    }
}
