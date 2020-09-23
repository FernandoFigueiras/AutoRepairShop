using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Helpers.Interfaces;
using AutoRepairShop.Web.Models.Account;
using AutoRepairShop.Web.Models.ActiveScheduleViewModel;
using AutoRepairShop.Web.Models.DShip;
using AutoRepairShop.Web.Models.EmployeeViewModel;
using AutoRepairShop.Web.Models.MainWindow;
using AutoRepairShop.Web.Models.VehicleViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Abstractions.Internal;
using Syncfusion.EJ2.Charts;
using System;
using System.Collections.Generic;
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
        private readonly IComboHelpers _comboHelpers;
        private readonly IServiceRepository _serviceRepository;
        private readonly IDealershipRepository _dealershipRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public ConverterHelper(IVehicleRepository vehicleRepository,
            IBrandRepository brandRepository,
            IFuelRepository fuelRepository,
            IColorRepository colorRepository,
            IUserHelper userHelper,
            IZipCodeRepository zipCodeRepository,
            ICountryRepository countryRepository,
            IComboHelpers comboHelpers,
            IServiceRepository serviceRepository,
            IDealershipRepository dealershipRepository,
            IDepartmentRepository departmentRepository)
        {
            _vehicleRepository = vehicleRepository;
            _brandRepository = brandRepository;
            _fuelRepository = fuelRepository;
            _colorRepository = colorRepository;
            _userHelper = userHelper;
            _zipCodeRepository = zipCodeRepository;
            _countryRepository = countryRepository;
            _comboHelpers = comboHelpers;
            _serviceRepository = serviceRepository;
            _dealershipRepository = dealershipRepository;
            _departmentRepository = departmentRepository;
        }




        public Vehicle ToNewVehicle(AddVehicleViewModel model, User user)
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
                User = user,
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
                Id = vehicle.Id,
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
                //ModelName = modelType.ModelName,
                EngineCapacity = vehicle.EngineCapacity,
                BrandId = vehicle.BrandId,
                Models = _vehicleRepository.GetComboModels(brand.Id),
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




        public User ToNewUserFromRegisterViewModel(RegisterViewModel model)
        {
            if (model == null)
            {
                return null;
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.UserName,
                ZipCodeId = 1,
                City = model.City,
            };

            if (user == null)
            {
                return null;
            }

            return user;

        }





        public UpdateUserDataViewModel ToUpdateDataViewModel(User user, ZipCode zipCode)
        {
            var model = new UpdateUserDataViewModel
            {
                User = user,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                ZipCode4 = zipCode.ZipCode4,
                ZipCode3 = zipCode.ZipCode3,
                City = user.City,
                TaxPayerNumber = user.TaxPayerNumber,
                PhoneNumber = user.PhoneNumber,
            };

            return model;
        }





        public User ToUserFromUpdate(UpdateUserDataViewModel model, User user, int zipCodeId, string path)
        {


            var updateUser = user;
            updateUser.FirstName = model.FirstName;
            updateUser.LastName = model.LastName;
            updateUser.Address = model.Address;
            updateUser.ZipCodeId = zipCodeId;
            updateUser.ImageUrl = path;
            updateUser.City = model.City;
            updateUser.PhoneNumber = model.PhoneNumber;
            updateUser.TaxPayerNumber = model.TaxPayerNumber;
            updateUser.IsActive = true;

            return updateUser;
        }





        public ResetPasswordViewModel ToResetPasswordViewModel(User user)
        {
            var model = new ResetPasswordViewModel
            {
                User = user,
            };

            return model;
        }






        public async Task<User> ToUserFromResetPasswordViewModel(ResetPasswordViewModel model)
        {
            return await _userHelper.GetUserByEmailAsync(model.User.UserName);
        }





        public async Task<User> ToUserFromEditUserResetPassword(UpdateUserDataViewModel model)
        {
            return await _userHelper.GetUserByEmailAsync(model.User.UserName);
        }





        public ChangePasswordViewModel ToChangePasswordViewModel(User user)
        {
            return new ChangePasswordViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
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





        public City ToNewCityModel(int id)
        {
            return new City
            {
                DistrictId = id,
                Id = 0,
            };
        }





        public City ToCity(City city, bool isNew)
        {
            if (isNew)
            {
                return new City
                {
                    Id = 0,
                    CityName = city.CityName,
                    DistrictId = city.DistrictId,
                };

            }
            else
            {
                return new City
                {
                    Id = city.Id,
                    CityName = city.CityName,
                    DistrictId = city.DistrictId,
                };

            }
        }





        public DealershipServicesViewModel ToDealershipViewModel(int dealershipId, string dealershipName, List<ServicesSupplied> services)
        {
            return new DealershipServicesViewModel
            {
                DealershipId = dealershipId,
                Services = services,
                DealershipName = dealershipName,
            };
        }





        public ServicesSupplied ToServicesSupplied(Dealership dealership, Service service, int? serviceSuppliedId, bool isActive)
        {
            return new ServicesSupplied
            {
                Id = serviceSuppliedId.Value,
                Dealership = dealership,
                Service = service,
                IsActive = isActive,
            };

        }





        public ServicesSupplied ToNewServicesSupplied(Dealership dealership, Service service)
        {
            return new ServicesSupplied
            {
                Dealership = dealership,
                Service = service,
                IsActive = false,
            };

        }





        public ZipCode ToNewZipCode(string zipcode4, string zipcode3, int cityId)
        {
            return new ZipCode
            {
                Id = 0,
                IsActive = true,
                ZipCode4 = zipcode4,
                ZipCode3 = zipcode3,
                CityId = cityId,
            };
        }





        public MainWindowViewModel ToMainWindowViewModelFromVehicles(User user)
        {
            return new MainWindowViewModel
            {
                UserImageUrl = user.ImageUrl,
            };
        }




        public BeginScheduleViewModel ToNewScheduleViewModel(IEnumerable<Vehicle> vehicles, IEnumerable<ServicesSupplied> services)
        {
            return new BeginScheduleViewModel
            {
                Vehicles = _comboHelpers.GetVehicles(vehicles),
                ServicesSupplied = _comboHelpers.GetServices(services),
               
            };
        }





        public async Task<ActiveSchedule> ToActiveSchedule(BeginScheduleViewModel model)
        {
            return new ActiveSchedule
            {
                IsActive = true,
                CreationDate = DateTime.Now,
                Services = await _serviceRepository.GetByIdAsync(model.ServicesSuppliedId)
            };
        }






        public ScheduleDetail ToScheduleDetail(Vehicle vehicle, ActiveSchedule activeSchedule, Dealership dealership)
        {
            return new ScheduleDetail
            {
                IsActive = true,
                CreationDate = DateTime.Now,
                Vehicle = vehicle,
                ActiveSchedule = activeSchedule,
                Dealership = dealership,
                
                
            };
        }





        public CompleteScheduleViewModel ToCompleteScheduleViewModel(BeginScheduleViewModel model, Service service)
        {
            return new CompleteScheduleViewModel
            {
                VehicleId = model.VehicleId,
                ServicesSuppliedId = model.ServicesSuppliedId,
                DealershipId = model.DealershipId,
                ServiceId = service.Id
               
            };

        }





        public ActiveSchedule ToActiveSchedule(CompleteScheduleViewModel model, Service service)
        {
            return new ActiveSchedule
            {
                IsActive = true,
                CreationDate = DateTime.Now,
                ScheduleDay = model.Day,
                Remarks = model.Remarks,
                Services = service,
                Mileage = model.Mileage
            };
        }





        public EditScheduleViewModel ToEditScheduleViewModel(ScheduleDetail scheduleDetail, IEnumerable<ServicesSupplied> services)
        {
            return new EditScheduleViewModel
            {
                LicencePlate = scheduleDetail.Vehicle.LicencePlate,
                Dealership = scheduleDetail.Dealership.DealerShipName,
                ServicesSupliedId = scheduleDetail.ActiveSchedule.Services.Id,
                Services = _comboHelpers.GetServices(services),
                Day = scheduleDetail.ActiveSchedule.ScheduleDay,
                Remarks = scheduleDetail.ActiveSchedule.Remarks,
            };
        }




        public async Task<ActiveSchedule> ToActiveScheduleFromEditAsync(EditScheduleViewModel model)
        {
            return new ActiveSchedule
            {
                Id = model.ActiveScheduleId,
                IsActive = true,
                UpdateDate = DateTime.Now,
                ScheduleDay = model.Day,
                Services = await _serviceRepository.GetByIdAsync(model.ServicesSupliedId),
                Remarks = model.Remarks,
            };
        }





        public async Task<ScheduleDetailsViewModel> ToScheduleDetailsViewModelAsync(ScheduleDetail scheduleDetail)
        {
            return new ScheduleDetailsViewModel
            {
                ActiveScheduleId = scheduleDetail.ActiveSchedule.Id,
                LicencePLate = scheduleDetail.Vehicle.LicencePlate,
                Brand = await _brandRepository.GetBrandNameByIdAsync(scheduleDetail.Vehicle.BrandId),
                Model = await _brandRepository.GetModelNameByIdAsync(scheduleDetail.Vehicle.ModelId),
                Dealership = scheduleDetail.Dealership.DealerShipName,
                Service = scheduleDetail.ActiveSchedule.Services.ServiceType,
                Date = scheduleDetail.ActiveSchedule.ScheduleDay,
                Remarks = scheduleDetail.ActiveSchedule.Remarks,

            };
        }




        public DeleteScheduleViewModel ToDeleteScheduleViewModel(ScheduleDetail scheduleDetail)
        {
            return new DeleteScheduleViewModel
            {
                ActiveScheduleId = scheduleDetail.ActiveSchedule.Id,
                LicencePLate = scheduleDetail.Vehicle.LicencePlate,
                Dealership = scheduleDetail.Dealership.DealerShipName,
                Service = scheduleDetail.ActiveSchedule.Services.ServiceType,
                ScheduleDay = scheduleDetail.ActiveSchedule.ScheduleDay,
            };
        }





        public CreateEmployeeViewModel ToCreateEmployeeVieModel(IEnumerable<Dealership> dealerships, IEnumerable<Department> departments)
        {
            return new CreateEmployeeViewModel
            {
                Dealerships = _comboHelpers.GetDealerships(dealerships),
                Departments = _comboHelpers.GetDepartments(departments),

            };
        }





        public User ToEmployeeUser(string userName,  User user)
        {
            return new User
            {
                UserName= userName,
                Email = userName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                ZipCodeId = user.ZipCodeId,
                City = user.City,
                PhoneNumber = user.PhoneNumber,
                TaxPayerNumber = user.TaxPayerNumber,
            };
        }





        public Employee ToNewEmplyee(Dealership dealership, Department department, User user)
        {
            return new Employee
            {
                IsActive = true,
                CreationDate = DateTime.Now,
                Dealership = dealership,
                User = user,
                Department = department,
            };
        }




        public EditEmployeeViewModel ToEditEmployeeViewModel(IEnumerable<Dealership> dealerships, IEnumerable<Department> departments, Employee employee, User user)
        {
            return new EditEmployeeViewModel
            {
                EmployeeId = employee.Id,
                Dealerships = _comboHelpers.GetDealerships(dealerships),
                DealershipId = employee.Dealership.Id,
                Departments = _comboHelpers.GetDepartments(departments),
                DepartmentId = employee.Department.Id,
                UserId = user.Id,
                OldRole = employee.Role,
                IsActive = employee.IsActive,
            };
        }




        public async Task<Employee> ToEmplyoyeeFromEditViewModelAsync(EditEmployeeViewModel model, User user)
        {
            return new Employee
            {
                Id = model.EmployeeId,
                IsActive = model.IsActive,
                Dealership = await _dealershipRepository.GetByIdAsync(model.DealershipId),
                Department = await _departmentRepository.GetByIdAsync(model.DepartmentId),
                User = user,
            };
        }

    }
}
