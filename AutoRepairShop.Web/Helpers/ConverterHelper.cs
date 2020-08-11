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
        private readonly IBrandRepository _brandRepository;
        private readonly IFuelRepository _fuelRepository;
        private readonly IColorRepository _colorRepository;

        public ConverterHelper(IVehicleRepository vehicleRepository,
            IBrandRepository brandRepository,
            IFuelRepository fuelRepository,
            IColorRepository colorRepository)
        {
           _vehicleRepository = vehicleRepository;
            _brandRepository = brandRepository;
            _fuelRepository = fuelRepository;
            _colorRepository = colorRepository;
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




        public async Task<DeleteVehicleViewModel> ToDeleteVehicleViewModelAsync(Vehicle vehicle)
        {
            var brand = await _brandRepository.GetByIdAsync(vehicle.BrandId);
            var modelType = await _brandRepository.GetModelByIdAsync(vehicle.ModelId);
            var fuel = await _fuelRepository.GetByIdAsync(vehicle.FuelId);
            var color = await _colorRepository.GetByIdAsync(vehicle.ColorId);

            var model = new DeleteVehicleViewModel
            {
                Id=vehicle.Id,
                LicencePlate = vehicle.LicencePlate,
                Brand = brand.BrandName,
                ModelName = modelType.ModelName,
                EngineCapacity = vehicle.EngineCapacity,
                FuelType = fuel.FuelType,
                ColorName = color.ColorName,
            }; 

            return model;
        }

        public async Task<EditVehicleViewModel> ToEditVehicleViewModelAsync(Vehicle vehicle)
        {
            var brand = await _brandRepository.GetByIdAsync(vehicle.BrandId);
            var modelType = await _brandRepository.GetModelByIdAsync(vehicle.ModelId);

            var model = new EditVehicleViewModel
            {
                Id = vehicle.Id,
                LicencePlate = vehicle.LicencePlate,
                BrandName = brand.BrandName,
                ModelName = modelType.ModelName,
                EngineCapacity = vehicle.EngineCapacity,
                BrandId = vehicle.BrandId,
                Models = _vehicleRepository.GetComboModels(),
                ModelId = vehicle.ModelId,
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

        public async Task<VehicleDetailsViewModel> ToVehicleDetailsViewModelAsync(Vehicle vehicle)
        {


            var brand = await _brandRepository.GetByIdAsync(vehicle.BrandId);
            var modelType = await _brandRepository.GetModelByIdAsync(vehicle.ModelId);
            var fuel = await _fuelRepository.GetByIdAsync(vehicle.FuelId);
            var color = await _colorRepository.GetByIdAsync(vehicle.ColorId);

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
