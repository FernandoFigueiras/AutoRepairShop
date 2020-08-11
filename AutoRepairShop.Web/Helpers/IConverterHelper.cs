using AutoRepairShop.Web.Data.Entities;
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

    }
}
