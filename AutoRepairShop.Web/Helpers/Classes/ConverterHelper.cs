using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Helpers.Interfaces;
using AutoRepairShop.Web.Models.Account;
using AutoRepairShop.Web.Models.VehicleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Helpers.Classes
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IFuelRepository _fuelRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IUserHelper _userHelper;
        private readonly IZipCodeRepository _zipCodeRepository;
        private readonly ICountryRepository _countryRepository;

        public ConverterHelper(IVehicleRepository vehicleRepository,
            IBrandRepository brandRepository,
            IFuelRepository fuelRepository,
            IColorRepository colorRepository,
            IUserHelper userHelper,
            IZipCodeRepository zipCodeRepository,
            ICountryRepository countryRepository)
        {
           _vehicleRepository = vehicleRepository;
            _brandRepository = brandRepository;
            _fuelRepository = fuelRepository;
            _colorRepository = colorRepository;
            _userHelper = userHelper;
            _zipCodeRepository = zipCodeRepository;
            _countryRepository = countryRepository;
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




        public User ToNewUserFromRegisterViewModel (RegisterViewModel model)
        {
            if (model == null)
            {
                return null;
            }

            var user = new User
            {
                UserName=model.UserName,
                Email = model.UserName,
                ZipCodeId=1,
            };

            if (user==null)
            {
                return null;
            }

            return user;

        }


        public UpdateUserDataViewModel ToUpdateDataViewModel(User user, ZipCode zipCode)
        {
            var model = new UpdateUserDataViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName=user.Email,
                FirstName = user.FirstName,
                LastName =user.LastName,
                Address=user.Address,
                ZipCode4=zipCode.ZipCode4,
                ZipCode3 = zipCode.ZipCode3,
                PhoneNumber=user.PhoneNumber,
            };

            return model;
        }



        public User ToUserFromUpdate (UpdateUserDataViewModel model, User user, int zipCodeId)
        {


            var updateUser = user;

            updateUser.Id = model.Id;
            updateUser.UserName = model.Email;
            updateUser.Email = model.Email;
            updateUser.FirstName = model.FirstName;
            updateUser.LastName = model.LastName;
            updateUser.Address = model.Address;
            updateUser.ZipCodeId = zipCodeId;
            updateUser.PhoneNumber = model.PhoneNumber;

            return updateUser;
        }



        public ResetPasswordViewModel ToResetPasswordViewModel(User user)
        {
            var model = new ResetPasswordViewModel { Id = user.Id, Email=user.UserName, UserName = user.UserName};

            return model;
        }




        public async Task<User> ToUserFromResetPasswordViewModel(ResetPasswordViewModel model)
        {
            return await _userHelper.GetUserByEmailAsync(model.UserName);
        }


       


        public ChangePasswordViewModel ToChangePasswordViewModel(User user)
        {
            return new ChangePasswordViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email=user.Email
            };
        }


        public District ToNewDistrictModel(int Id)
        {
            return new District
            {
                CountryId = Id,
                Id = 0,
            };
        }



        public District ToDistrict(District district, bool isNew)
        {

            if (isNew)
            {
                return new District
                {
                    DistrictName = district.DistrictName,
                    CountryId = district.CountryId,
                    Id = 0,
                };
            }
            else
            {
                return new District
                {
                    DistrictName = district.DistrictName,
                    CountryId = district.CountryId,
                    Id = district.Id,
                };
            }
            

        }




    }
}
