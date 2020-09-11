using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Models.MainWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Models.VehicleViewModels
{
    public class ListAllVehicles : MainWindowViewModel
    {
        //public Vehicle Vehicle  { get; set; }


        public IEnumerable<Vehicle> Vehicles { get; set; }

    }
}
