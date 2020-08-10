using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Models.VehicleViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {



        IQueryable GetVehiclesWithBrandModelFuelAndColor();





        IEnumerable<SelectListItem> GetComboBrands();





        IEnumerable<SelectListItem> GetComboModels();





        IEnumerable<SelectListItem> GetComboFuels();





        IEnumerable<SelectListItem> GetComboColors();




        Task<SelectListItem> GetComboSoloBrand(AddVehicleViewModel model);

    }


}
