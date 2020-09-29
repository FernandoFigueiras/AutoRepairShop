using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Helpers.Interfaces;
using AutoRepairShop.Web.Models.RepairViewModels;
using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairShop.Web.Controllers.BackOffice
{
    public class RepairsController : Controller
    {
        private readonly IScheduleDetailRepository _scheduleDetailRepository;
        private readonly IDealershipDepartmentRepository _dealershipDepartmentRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IRepairScheduleRepository _repairScheduleRepository;
        private readonly IActiveScheduleRepository _activeScheduleRepository;
        private readonly IUserHelper _userHelper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRepairRepository _repairRepository;
        private readonly IRepairHistoryRepository _repairHistoryRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMailHelper _mailHelper;

        public RepairsController(IScheduleDetailRepository scheduleDetailRepository,
            IDealershipDepartmentRepository dealershipDepartmentRepository,
            IConverterHelper converterHelper,
            IRepairScheduleRepository repairScheduleRepository,
            IActiveScheduleRepository activeScheduleRepository,
            IUserHelper userHelper,
            IEmployeeRepository employeeRepository,
            IRepairRepository repairRepository,
            IRepairHistoryRepository repairHistoryRepository,
            IVehicleRepository vehicleRepository,
            IMailHelper mailHelper)
        {
            _scheduleDetailRepository = scheduleDetailRepository;
            _dealershipDepartmentRepository = dealershipDepartmentRepository;
            _converterHelper = converterHelper;
            _repairScheduleRepository = repairScheduleRepository;
            _activeScheduleRepository = activeScheduleRepository;
            _userHelper = userHelper;
            _employeeRepository = employeeRepository;
            _repairRepository = repairRepository;
            _repairHistoryRepository = repairHistoryRepository;
            _vehicleRepository = vehicleRepository;
            _mailHelper = mailHelper;
        }


        [Authorize(Roles = "Employee/Management, Employee/Reception, Admin, Customer")]
        public async Task<IActionResult> Index()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var repair = _repairScheduleRepository.GetUserRepairs(user.Id);
            return View(repair);
        }


        [Authorize(Roles = "Employee/Management, Employee/Reception, Employee/Mechanics, Employee/Electronic,Employee/Paint, Admin")]
        public async Task<IActionResult> DealershipRepairs()
        {

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var employee = await _employeeRepository.GetFullEmployeeByUserAsync(user.Id);
            var repair = _repairScheduleRepository.GetDealershipRepairs(employee.Dealership.Id);

            return View(repair);
        }

        [Authorize(Roles = "Employee/Management, Employee/Reception, Employee/Mechanics, Employee/Electronic,Employee/Paint")]
        public async Task<IActionResult> StartRepair(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }



            var scheduleDetails = await _scheduleDetailRepository.GetScheduleDetailAsync(Id.Value);

            if (scheduleDetails == null)
            {
                return NotFound();
            }


            var departments = _dealershipDepartmentRepository.GetDealershipDepartments(scheduleDetails.Dealership.Id);

            var model = _converterHelper.ToStartRepairViewModel(scheduleDetails, departments);


            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> StartRepair(StartRepairViewModel model)
        {


            var scheduleDetail = await _scheduleDetailRepository.GetScheduleDetailByActiveSchedule(model.ActiveScheduleId);

            if (scheduleDetail == null)
            {
                return NotFound();
            }

            var activeSchedule = await _activeScheduleRepository.GetByIdAsync(model.ActiveScheduleId);
            if (activeSchedule == null)
            {
                return NotFound();
            }


            var repair = await _converterHelper.ToRepairAsync(model);

            var repairSchedule = _converterHelper.ToRepairSchedule(scheduleDetail, repair);

            try
            {
                repair.CreationDate = DateTime.Now;
                await _repairScheduleRepository.CreateAsync(repairSchedule);
                scheduleDetail.IsActive = false;
                activeSchedule.IsActive = false;
                await _scheduleDetailRepository.UpdateAsync(scheduleDetail);
                await _activeScheduleRepository.UpdateAsync(activeSchedule);
                return RedirectToAction("ShowScheduleForDealership","ScheduleDetails");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                var scheduleDetailsEx = await _scheduleDetailRepository.GetScheduleDetailAsync(model.Schedule.Id);
                var departmentsEx = _dealershipDepartmentRepository.GetDealershipDepartments(scheduleDetailsEx.Dealership.Id);
                var returnModelEx = _converterHelper.ToStartRepairViewModel(scheduleDetailsEx, departmentsEx);

                return View(returnModelEx);
            }

        }


        [Authorize(Roles = "Employee/Management, Employee/Reception, Employee/Mechanics, Employee/Electronic, Employee/Paint ")]
        public async Task<IActionResult> EditRepair(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairSchedule = _repairScheduleRepository.GetRepairSchedule(id.Value);

            if (repairSchedule == null)
            {
                return NotFound();
            }

            var repair = await _repairScheduleRepository.GetRepairInfoByIdAsync(id.Value);

            return View(repair);
        }



        [HttpPost]
        public async Task<IActionResult> EditRepair(RepairSchedule repairSchedule)
        {
            if (ModelState.IsValid)
            {
                var repair = repairSchedule.Repair;

                if (repair == null)
                {
                    return NotFound();
                }

                await _repairRepository.UpdateAsync(repair);

                return RedirectToAction("DealershipRepairs");
            }

            return View(repairSchedule);

        }



        [Authorize(Roles = "Employee/Management, Employee/Reception, Employee/Mechanics, Employee/Electronic, Employee/Paint")]
        public async Task<IActionResult> FinishRepair(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var repairSchedule = await _repairScheduleRepository.GetRepairScheduleFinishAsync(id.Value);


            if (repairSchedule == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToFinishRepairViewModel(repairSchedule);


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FinishRepair(FinishRepairViewModel model)
        {
            if (model == null)
            {
                return View(model);
            }

            var repairShedule = await _repairScheduleRepository.GetRepairScheduleFinishAsync(model.RepairScheduleID);

            var scheduleDetail = await _scheduleDetailRepository.GetScheduleDetailByIdAsync(model.ScheduleDetailId);

            var repair = await _repairRepository.GetByIdAsync(model.RepairId);

            var activeSchedule = await _activeScheduleRepository.GetByIdAsync(scheduleDetail.ActiveSchedule.Id);

            var repairHistory = _converterHelper.ToRepairHistory(repairShedule, scheduleDetail);

            var vehicle = await _vehicleRepository.GetUserVehicle(model.VehicleId);

            var user = await _userHelper.GetUserByIdAsync(vehicle.User.Id);


            
            
            try
            {
                await _repairHistoryRepository.CreateAsync(repairHistory);
                await _repairScheduleRepository.DeleteAsync(repairShedule);
                await _scheduleDetailRepository.DeleteAsync(scheduleDetail);
                await _repairRepository.DeleteAsync(repair);
                await _activeScheduleRepository.DeleteAsync(activeSchedule);
                return RedirectToAction("DealershipRepairs");
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException.Message);
            }

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to send email for the user");
            }
            else
            {
                _mailHelper.SendEmail(user.UserName, "Repair finished", $"<h1>Your repair information</h1></br><p>Your vehicle {vehicle.LicencePlate} is ready to be picked up at the workshop</p> </br>");
                ViewBag.Message = "An email was sent to the user with this information";
            }

            

            return View(model);


        }

    }
}
