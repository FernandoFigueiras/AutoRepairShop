using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoRepairShop.Web.Data;
using AutoRepairShop.Web.Data.Entities;
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
        private readonly IDealershipRepository _dealershipRepository;
        private readonly IZipCodeRepository _zipCodeRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;

        public EmployeesController(IEmployeeRepository employeeRepository,
            IConverterHelper converterHelper,
            IDepartmentRepository departmentRepository,
            IDealershipRepository dealershipRepository,
            IZipCodeRepository zipCodeRepository,
            ICityRepository cityRepository,
            IUserHelper userHelper,
            IMailHelper mailHelper)
        {
            _employeeRepository = employeeRepository;
            _converterHelper = converterHelper;
            _departmentRepository = departmentRepository;
            _dealershipRepository = dealershipRepository;
            _zipCodeRepository = zipCodeRepository;
            _cityRepository = cityRepository;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
        }






        public IActionResult Index()
        {
            var employees = _employeeRepository.GetEmployeesFullInfoAsync();
            return View(employees);
        }






        public IActionResult CreateEmployee()
        {
            var dealerships = _dealershipRepository.GetAll();
            var departments = _departmentRepository.GetAll();

            var model = _converterHelper.ToCreateEmployeeVieModel(dealerships,departments);

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


                    var employee = _converterHelper.ToNewEmplyee(dealership, department, user);
                    
                    employee.CreationDate = DateTime.Now;

                    try
                    {
                        employee.IsActive = true;
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

                this.ModelState.AddModelError(string.Empty, $"the user {user.UserName} allready exists");
            }



            return View(model);
        }





        public async Task<IActionResult> DeleteEmployee(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetEmployeeFullInfoAsync(Id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }





        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int Id)
        {

            var employee = await _employeeRepository.GetEmployeeFullInfoAsync(Id);


            if (employee == null)
            {
                return NotFound();
            }

            try
            {
                await _employeeRepository.DeleteAsync(employee);
                var user = await _userHelper.GetUserByEmailAsync(employee.User.UserName);
                var response = await _userHelper.DeleteUserAsync(user);

                if (response.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Unable to delete user");
                    await _userHelper.RemoveFromRoleAsync(user, employee.Role);
                }
                

                
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("REFERENCE constraint"))
                {

                    if (ModelState.IsValid)
                    {
                        ViewBag.Error = $"Unable to delete the employee named {employee.User.FullName}, contact system Admin";
                        return View(employee);
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    return View(employee);
                }
            }

            return View(employee);
        }






        public async Task<IActionResult> EditEmployee(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetEmployeeFullInfoAsync(Id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            var dealerships = _dealershipRepository.GetAll();
            var departments = _departmentRepository.GetAll();

            var user = await _userHelper.GetUserByIdAsync(employee.User.Id);


            var model = _converterHelper.ToEditEmployeeViewModel(dealerships, departments, employee, user);

            return View(model);
        }





        [HttpPost]
        public async Task<IActionResult> EditEmployee(EditEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByIdAsync(model.UserId);
                var employee = await _converterHelper.ToEmplyoyeeFromEditViewModelAsync(model, user);
                
                try
                {
                    if (model.IsActive ==false)
                    {
                        employee.IsActive = false;
                    }

                    await _employeeRepository.UpdateAsync(employee);
                    await _userHelper.RemoveFromRoleAsync(user, model.OldRole);
                    var response = await _userHelper.IsUSerInRoleAsync(user, employee.Role);

                    if (!response)
                    {
                       await _userHelper.CheckRoleAsync(employee.Role);

                        await _userHelper.AddUserToRoleAsync(user, employee.Role);
                    }

                    return RedirectToAction(nameof(Index));
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

            }

            return View(model);
        }



        public async Task<IActionResult> DetailsEmployee(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetEmployeeFullInfoAsync(Id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
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
