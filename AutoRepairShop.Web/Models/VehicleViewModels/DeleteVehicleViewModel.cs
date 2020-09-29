using AutoRepairShop.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.VehicleViewModels
{
    public class DeleteVehicleViewModel : Vehicle
    {

        public string Brand { get; set; }




       
        [Display(Name ="Model")]
        public string ModelName { get; set; }




        
        [Display(Name ="Fuel")]
        public string FuelType { get; set; }

        

        
        
        [Display(Name ="Color")]
        public string ColorName { get; set; }

    }
}
