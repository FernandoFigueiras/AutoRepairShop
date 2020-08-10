using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories;
using AutoRepairShop.Web.Models.VehicleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly IVehicleRepository _vehicleRepository;

        public ConverterHelper(IVehicleRepository vehicleRepository)
        {
           _vehicleRepository = vehicleRepository;
        }


        public Vehicle ToNewVehicle(AddVehicleViewModel model)
        {
            var vehicle = new Vehicle
            {
                LicencePlate = model.LicencePlate,
                BrandId = model.BrandId,
                ModelId = model.ModelId,
                EngineCapacity = model.EngineCapacity,
                FuelId = model.FuelId,
                ColorId = model.ColorId,
                CreationDate = DateTime.UtcNow,
            };

            return vehicle;
        }




        public DeleteVehicleViewModel ToDeleteVehicleViewModel(Vehicle vehicle, Brand brand, Model modelType, Fuel fuel, Color color)
        {
            var model = new DeleteVehicleViewModel
            {
                LicencePlate = vehicle.LicencePlate,
                Brand = brand.BrandName,
                ModelName = modelType.ModelName,
                EngineCapacity = vehicle.EngineCapacity,
                FuelType = fuel.FuelType,
                Color = vehicle.Color
            };

            return model;
        }

        public EditVehicleViewModel ToEditVehicleViewModel(Vehicle vehicle, Brand brand, Model modelType, Fuel fuel, Color color)
        {

            var model = new EditVehicleViewModel
            {
                LicencePlate = vehicle.LicencePlate,
                BrandName = brand.BrandName,
                ModelName = modelType.ModelName,
                EngineCapacity = vehicle.EngineCapacity,
                BrandId = brand.Id,
                Models = _vehicleRepository.GetComboModels(),
                ModelId = modelType.Id,
                FuelId = vehicle.FuelId,
                Fuels = _vehicleRepository.GetComboFuels(),
                ColorId = vehicle.ColorId,
                Colors = _vehicleRepository.GetComboColors(),
            };

            return model;
            
        }

        public Vehicle ToEditVehicle(EditVehicleViewModel model)
        {
            var vehicle = new Vehicle
            {
                Id = model.Id,
                LicencePlate = model.LicencePlate,
                BrandId = model.BrandId,
                ModelId = model.ModelId,
                EngineCapacity = model.EngineCapacity,
                FuelId = model.FuelId,
                ColorId = model.ColorId,
                UpdateDate = DateTime.UtcNow,
            };

            return vehicle;

        }

        public VehicleDetailsViewModel ToVehicleDetailsViewModel(Vehicle vehicle, Brand brand, Model modelType, Fuel fuel, Color color)
        {
            var model = new VehicleDetailsViewModel
            {
                Id = vehicle.Id,
                LicencePlate = vehicle.LicencePlate,
                BrandName = brand.BrandName,
                ModelName = modelType.ModelName,
                EngineCapacity = vehicle.EngineCapacity,
                Fueltype = fuel.FuelType,
                ColorName = color.ColorName,
            };

            return model;
        }
    }
}
