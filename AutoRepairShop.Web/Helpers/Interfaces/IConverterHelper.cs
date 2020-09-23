using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Models.Account;
using AutoRepairShop.Web.Models.ActiveScheduleViewModel;
using AutoRepairShop.Web.Models.DShip;
using AutoRepairShop.Web.Models.EmployeeViewModel;
using AutoRepairShop.Web.Models.MainWindow;
using AutoRepairShop.Web.Models.VehicleViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Helpers.Interfaces
{
    public interface IConverterHelper
    {


        Vehicle ToNewVehicle(AddVehicleViewModel model, User user);




        Vehicle ToEditVehicle(EditVehicleViewModel model);




        Task<DeleteVehicleViewModel> ToDeleteVehicleViewModelAsync(Vehicle vehicle);




        Task<EditVehicleViewModel> ToEditVehicleViewModelAsync(Vehicle vehicle);




        Task<VehicleDetailsViewModel> ToVehicleDetailsViewModelAsync(Vehicle vehicle);




        User ToNewUserFromRegisterViewModel(RegisterViewModel model);



        UpdateUserDataViewModel ToUpdateDataViewModel(User user, ZipCode zipCode);



        User ToUserFromUpdate(UpdateUserDataViewModel model,User user, int zipCodeId, string path);



        ResetPasswordViewModel ToResetPasswordViewModel(User user);




        Task<User> ToUserFromResetPasswordViewModel(ResetPasswordViewModel model);





        Task<User> ToUserFromEditUserResetPassword(UpdateUserDataViewModel model);





        ChangePasswordViewModel ToChangePasswordViewModel(User user);




        District ToNewDistrictModel(int Id);




        District ToDistrict(District district, bool isNew);



        City ToNewCityModel(int id);




        City ToCity(City city, bool isNew);




        DealershipServicesViewModel ToDealershipViewModel(int dealershipId, string dealershipName, List<ServicesSupplied> services);




        ServicesSupplied ToServicesSupplied(Dealership dealership, Service service, int? serviceSuppliedId, bool isActive);




        ServicesSupplied ToNewServicesSupplied(Dealership dealership, Service service);




        ZipCode ToNewZipCode(string zipcode4, string zipcode3, int cityId);




        MainWindowViewModel ToMainWindowViewModelFromVehicles(User user);




        BeginScheduleViewModel ToNewScheduleViewModel(IEnumerable<Vehicle> vehicles, IEnumerable<ServicesSupplied> services);




        Task<ActiveSchedule> ToActiveSchedule(BeginScheduleViewModel model);




        ScheduleDetail ToScheduleDetail(Vehicle vehicle, ActiveSchedule activeSchedule, Dealership dealership);



        CompleteScheduleViewModel ToCompleteScheduleViewModel(BeginScheduleViewModel model, Service service);



        ActiveSchedule ToActiveSchedule(CompleteScheduleViewModel model, Service service);



        EditScheduleViewModel ToEditScheduleViewModel(ScheduleDetail scheduleDetail, IEnumerable<ServicesSupplied> services);




        Task<ActiveSchedule> ToActiveScheduleFromEditAsync(EditScheduleViewModel model);



        Task<ScheduleDetailsViewModel> ToScheduleDetailsViewModelAsync(ScheduleDetail scheduleDetail);



        DeleteScheduleViewModel ToDeleteScheduleViewModel(ScheduleDetail scheduleDetail);



        CreateEmployeeViewModel ToCreateEmployeeVieModel(IEnumerable<Dealership> dealerships, IEnumerable<Department> departments);



        User ToEmployeeUser(string userName,  User user);



        Employee ToNewEmplyee(Dealership dealership, Department department, User user);



        EditEmployeeViewModel ToEditEmployeeViewModel(IEnumerable<Dealership> dealerships, IEnumerable<Department> departments, Employee employee, User user);



        Task<Employee> ToEmplyoyeeFromEditViewModelAsync(EditEmployeeViewModel model, User user);

    }
}
