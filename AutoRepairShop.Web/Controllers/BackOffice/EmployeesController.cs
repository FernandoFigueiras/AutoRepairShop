using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoRepairShop.Web.Data;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Helpers.Interfaces;
using AutoRepairShop.Web.Models.EmployeeViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairShop.Web.Controllers.BackOffice
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeePositionRepository _employeePositionRepository;
        private readonly IDealershipRepository _dealershipRepository;

        public EmployeesController(IEmployeeRepository employeeRepository,
            IConverterHelper converterHelper,
            IDepartmentRepository departmentRepository,
            IEmployeePositionRepository employeePositionRepository,
            IDealershipRepository dealershipRepository)
        {
            _employeeRepository = employeeRepository;
            _converterHelper = converterHelper;
            _departmentRepository = departmentRepository;
            _employeePositionRepository = employeePositionRepository;
            _dealershipRepository = dealershipRepository;
        }

        public IActionResult Index()
        {
            var employees = _employeeRepository.GetEmployeeFullInfoAsync();
            return View(employees);
        }


        public IActionResult CreateEmployee()
        {
            var dealerships = _dealershipRepository.GetAll();
            var departments = _departmentRepository.GetAll();
            var position = _employeePositionRepository.GetAll();

            var model = _converterHelper.ToCreateEmployeeVieModel(dealerships,departments, position);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeViewModel model)
        {
            return View();
        }
    }
}
