using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Models.Account;
using AutoRepairShop.Web.Models.VehicleViewModels;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Helpers.Interfaces
{
    public interface IConverterHelper
    {


        Vehicle ToNewVehicle(AddVehicleViewModel model);


        Vehicle ToEditVehicle(EditVehicleViewModel model);


        Task<DeleteVehicleViewModel> ToDeleteVehicleViewModelAsync(Vehicle vehicle);


        Task<EditVehicleViewModel> ToEditVehicleViewModelAsync(Vehicle vehicle);


        Task<VehicleDetailsViewModel> ToVehicleDetailsViewModelAsync(Vehicle vehicle);


        User ToNewUserFromRegisterViewModel(RegisterViewModel model);



        UpdateUserDataViewModel ToUpdateDataViewModel(User user, ZipCode zipCode);


        User ToUserFromUpdate(UpdateUserDataViewModel model, User user, int zipCodeId);



        ResetPasswordViewModel ToResetPasswordViewModel(User user);




        Task<User> ToUserFromResetPasswordViewModel(ResetPasswordViewModel model);



        ChangePasswordViewModel ToChangePasswordViewModel(User user);


        District ToNewDistrictModel(int Id);


        District ToDistrict(District district, bool isNew);


        City ToNewCityModel(int id);


        City ToCity(City city, bool isNew);


    }
}
