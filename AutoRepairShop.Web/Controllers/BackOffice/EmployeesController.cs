using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoRepairShop.Web.Data;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Helpers.Interfaces;
using AutoRepairShop.Web.Models.EmployeeViewModel;
using Microsoft.AspNetCore.Identity;
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
        private readonly IZipCodeRepository _zipCodeRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;

        public EmployeesController(IEmployeeRepository employeeRepository,
            IConverterHelper converterHelper,
            IDepartmentRepository departmentRepository,
            IEmployeePositionRepository employeePositionRepository,
            IDealershipRepository dealershipRepository,
            IZipCodeRepository zipCodeRepository,
            ICityRepository cityRepository,
            IUserHelper userHelper,
            IMailHelper mailHelper)
        {
            _employeeRepository = employeeRepository;
            _converterHelper = converterHelper;
            _departmentRepository = departmentRepository;
            _employeePositionRepository = employeePositionRepository;
            _dealershipRepository = dealershipRepository;
            _zipCodeRepository = zipCodeRepository;
            _cityRepository = cityRepository;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
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
            if (ModelState.IsValid)
            {
                var user = _converterHelper.ToEmployeeUser(model.UserName, model.User);

                if (await _userHelper.GetUserByEmailAsync(user.UserName) == null)
                {

                    user.IsActive = true;
                    

                   var result = await _userHelper.AddUserAsync(user, model.Password);


                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user could not be created");
                        return this.View(model);
                    }

                    var dealership = await _dealershipRepository.GetByIdAsync(model.DealershipId);

                    var department = await _departmentRepository.GetByIdAsync(model.DepartmentId);

                    var position = await _employeePositionRepository.GetByIdAsync(model.PositionId);

                    var employee = _converterHelper.ToNewEmplyee(dealership, department, user, position);

                    try
                    {
                        await _employeeRepository.CreateAsync(employee);
                        

                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.Message.Contains("duplicate"))
                        {


                            ModelState.AddModelError(string.Empty, $"There is allready a Employee registered with the name {user.FullName} please insert another");
                            return View(model);

                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                            return View(model);
                        }
                    }

                    await _userHelper.CheckRoleAsync(employee.Role);

                    var userRole = await _userHelper.IsUSerInRoleAsync(user, employee.Role);

                    if (!userRole)
                    {
                        await _userHelper.AddUserToRoleAsync(user, employee.Role);
                    }


                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);


                    var tokenLink = this.Url.Action("ConfirmEmail", "Accounts", new
                    {
                        userId = user.Id,
                        token = myToken,
                    }, protocol: HttpContext.Request.Scheme);

                    _mailHelper.SendEmail(user.UserName, "Email Confirmation", $"<h1>Email Confirmation</h1>" +
                      $"To allow the user, " +
                      $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>" +
                      $"<p>Your password is {model.Password}, you must change it in first login</p>");

                    this.ViewBag.Message = "The instructions for completing registration have been sent by email for the user";

                    return View(model);

                }

                this.ModelState.AddModelError(string.Empty, "The user already exists");
                return View(model);
            }



            return View(model);
        }








        public async Task<JsonResult> GetZipCodeAndCityId(string zip4, string zip3)
        {
            var exists = await _zipCodeRepository.ZipCodeExistsAsync(zip4, zip3);

            if (exists)
            {
                var zipcode = await _zipCodeRepository.GetZipCodeAsync(zip4, zip3);

                var cityName = await _cityRepository.GetByIdAsync(zipcode.CityId);

                string data = zipcode.Id + "," + cityName.CityName;

                var r = this.Json(data);

                return r;
            }

            return null;
        }
    }
}
