using AutoRepairShop.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AutoRepairShop.Web.Helpers.Interfaces
{
    public interface IComboHelpers
    {
        IEnumerable<SelectListItem> GetServices(IEnumerable<DealershipService> services);



        IEnumerable<SelectListItem> GetallServices(IEnumerable<Service> services);



        IEnumerable<SelectListItem> GetVehicles(IEnumerable<Vehicle> vehicles);




        IEnumerable<SelectListItem> GetDealerships(IEnumerable<Dealership> dealerships);




        IEnumerable<SelectListItem> GetDepartments(IEnumerable<Department> departments);



        IEnumerable<SelectListItem> GetDealershipDepartments(IEnumerable<DealershipDepartment> departments);

    }
}
