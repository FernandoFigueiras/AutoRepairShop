using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Models.MainWindow;
using System.Collections.Generic;

namespace AutoRepairShop.Web.Models.VehicleViewModels
{
    public class ListAllVehicles : MainWindowViewModel
    {


        public int VehicleId { get; set; }



        public IEnumerable<Vehicle> Vehicles { get; set; }

    }
}
