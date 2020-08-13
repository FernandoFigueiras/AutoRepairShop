using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Models.Account;
using AutoRepairShop.Web.Models.VehicleViewModels;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Helpers
{
    public interface IConverterHelper
    {


        Vehicle ToNewVehicle(AddVehicleViewModel model);


        Vehicle ToEditVehicle(EditVehicleViewModel model);


        Task<DeleteVehicleViewModel> ToDeleteVehicleViewModelAsync(Vehicle vehicle);


        Task<EditVehicleViewModel> ToEditVehicleViewModelAsync(Vehicle vehicle);


        Task<VehicleDetailsViewModel> ToVehicleDetailsViewModelAsync(Vehicle vehicle);


        User ToNewUserFromRegisterViewModel(RegisterViewModel model);



        UpdateUserDataViewModel ToUpdateDataViewModel(User user);


        User ToUserFromUpdate(UpdateUserDataViewModel model, User user);



        ResetPasswordViewModel ToResetPasswordViewModel(User user);




        Task<User> ToUserFromResetPasswordViewModel(ResetPasswordViewModel model);



        ChangePasswordViewModel ToChangePasswordViewModel(User user);

    }
}
