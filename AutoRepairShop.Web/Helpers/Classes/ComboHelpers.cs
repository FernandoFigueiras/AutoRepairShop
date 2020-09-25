using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Helpers.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace AutoRepairShop.Web.Helpers.Classes
{
    public class ComboHelpers : IComboHelpers
    {

        public IEnumerable<SelectListItem> GetServices(IEnumerable<DealershipService> services)
        {
            var list = services.Select(s => new SelectListItem
            {
                Text = s.Service.ServiceType,
                Value = s.Service.Id.ToString(),
            }).ToList();

            if (list.Count > 1)
            {
                list.Insert(0, new SelectListItem
                {
                    Text = "Select a Service...",
                    Value = "0"
                });
            }

            return list;

        }



        public IEnumerable<SelectListItem> GetVehicles(IEnumerable<Vehicle> vehicles)
        {
            var list = vehicles.Select(s => new SelectListItem
            {
                Text = s.LicencePlate,
                Value = s.Id.ToString(),
            }).ToList();

            if (list.Count > 1)
            {
                list.Insert(0, new SelectListItem
                {
                    Text = "Select a car...",
                    Value = "0"
                });
            }

            return list;
        }


        public IEnumerable<SelectListItem> GetDealerships(IEnumerable<Dealership> dealerships)
        {
            var list = dealerships.Select(d => new SelectListItem
            {
                Text = d.DealerShipName,
                Value = d.Id.ToString(),
            }).ToList();

            if (list.Count > 0)
            {
                list.Insert(0, new SelectListItem
                {
                    Text = "Select a dealership...",
                    Value = "=",
                });
            }

            return list;
        }

        public IEnumerable<SelectListItem> GetDepartments(IEnumerable<Department> departments)
        {
            var list = departments.Select(d => new SelectListItem
            {
                Text = d.DepartmentName,
                Value = d.Id.ToString(),
            }).ToList();


            if (list.Count > 0)
            {
                list.Insert(0, new SelectListItem
                {
                    Text = "Select a department...",
                    Value = "0",
                });
            }

            return list;
        }

        public IEnumerable<SelectListItem> GetDealershipDepartments(IEnumerable<DealershipDepartment> departments)
        {
            var list = departments.Select(d => new SelectListItem
            {
                Text = d.Department.DepartmentName,
                Value = d.Department.Id.ToString(),
            }).ToList();


            if (list.Count > 0)
            {
                list.Insert(0, new SelectListItem
                {
                    Text = "Select a department...",
                    Value = "0",
                });
            }

            return list;
        }

        public IEnumerable<SelectListItem> GetDealershipsEdit(IEnumerable<Dealership> dealerships)
        {
            var list = dealerships.Select(d => new SelectListItem
            {
                Text = d.DealerShipName,
                Value = d.Id.ToString(),
            }).ToList();

            

            return list;
        }

        public IEnumerable<SelectListItem> GetDepartmentsEdit(IEnumerable<Department> departments)
        {
            var list = departments.Select(d => new SelectListItem
            {
                Text = d.DepartmentName,
                Value = d.Id.ToString(),
            }).ToList();


            

            return list;
        }






    }
}
