using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Helpers.Interfaces;
using AutoRepairShop.Web.Models.RepairViewModels;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IDealershipRepository _dealershipRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRepairRepository _repairRepository;

        public RepairsController(IScheduleDetailRepository scheduleDetailRepository,
            IDealershipDepartmentRepository dealershipDepartmentRepository,
            IConverterHelper converterHelper,
            IRepairScheduleRepository repairScheduleRepository,
            IActiveScheduleRepository activeScheduleRepository,
            IUserHelper userHelper,
            IDealershipRepository dealershipRepository,
            IEmployeeRepository employeeRepository,
            IRepairRepository repairRepository)
        {
            _scheduleDetailRepository = scheduleDetailRepository;
            _dealershipDepartmentRepository = dealershipDepartmentRepository;
            _converterHelper = converterHelper;
            _repairScheduleRepository = repairScheduleRepository;
            _activeScheduleRepository = activeScheduleRepository;
            _userHelper = userHelper;
            _dealershipRepository = dealershipRepository;
            _employeeRepository = employeeRepository;
            _repairRepository = repairRepository;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        public async Task<IActionResult> DealershipRepairs()
        {

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var employee = await _employeeRepository.GetFullEmployeeByUserAsync(user.Id);
            var repair = _repairScheduleRepository.GetDealershipRepairs(employee.Dealership.Id);

            return View(repair);
        }

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


            var departments = _dealershipDepartmentRepository.GetDealershipDepartmentsAsync(scheduleDetails.Dealership.Id);

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
                var departmentsEx = _dealershipDepartmentRepository.GetDealershipDepartmentsAsync(scheduleDetailsEx.Dealership.Id);
                var returnModelEx = _converterHelper.ToStartRepairViewModel(scheduleDetailsEx, departmentsEx);

                return View(returnModelEx);
            }

        }

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



        
        public async Task<IActionResult> FinishRepair(int? id)
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

            return View();
        }
    }
}
