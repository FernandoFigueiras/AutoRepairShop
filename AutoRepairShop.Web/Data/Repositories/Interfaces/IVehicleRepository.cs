using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Models.VehicleViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {



        IEnumerable<Vehicle> GetUserVehicles(string userID);




        Task<Vehicle> GetUserVehicle(int id);




        IEnumerable<SelectListItem> GetComboBrands();





        IEnumerable<SelectListItem> GetComboModels(int brandId);





        IEnumerable<SelectListItem> GetComboFuels();





        IEnumerable<SelectListItem> GetComboColors();




        Task<SelectListItem> GetComboSoloBrand(AddVehicleViewModel model);




        Task<Vehicle> GetVehicleByUserIdAsync(string userId);




        Task<Vehicle> GetVehicleByLicencePlateAsync(string licencePlate);
    }


}
