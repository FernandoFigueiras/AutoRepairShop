using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Models.VehicleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Helpers
{
    public interface IConverterHelper
    {


        Vehicle ToNewVehicle(AddVehicleViewModel model);


        Vehicle ToEditVehicle(EditVehicleViewModel model);


        DeleteVehicleViewModel ToDeleteVehicleViewModel(Vehicle vehicle, Brand brand, Model modelType, Fuel fuel, Color color);

        EditVehicleViewModel ToEditVehicleViewModel(Vehicle vehicle, Brand brand, Model modelType, Fuel fuel, Color color);


        VehicleDetailsViewModel ToVehicleDetailsViewModel(Vehicle vehicle, Brand brand, Model modelType, Fuel fuel, Color color);

    }
}
