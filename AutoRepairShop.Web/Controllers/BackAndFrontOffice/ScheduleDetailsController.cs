﻿using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Helpers.Interfaces;
using AutoRepairShop.Web.Models.ActiveScheduleViewModel;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Controllers.BackAndFrontOffice
{
    public class ScheduleDetailsController : Controller
    {
        private readonly IActiveScheduleRepository _activeScheduleRepository;
        private readonly IUserHelper _userHelper;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IServicesSuppliedRepository _servicesSuppliedRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IDealershipRepository _dealershipRepository;
        private readonly IScheduleDetailRepository _scheduleDetailRepository;
        private readonly IServiceRepository _serviceRepository;

        public ScheduleDetailsController(IActiveScheduleRepository activeScheduleRepository,
            IUserHelper userHelper,
            IVehicleRepository vehicleRepository,
            IServicesSuppliedRepository servicesSuppliedRepository,
            IConverterHelper converterHelper,
            IDealershipRepository dealershipRepository,
            IScheduleDetailRepository scheduleDetailRepository,
            IServiceRepository serviceRepository)
        {
            _activeScheduleRepository = activeScheduleRepository;
            _userHelper = userHelper;
            _vehicleRepository = vehicleRepository;
            _servicesSuppliedRepository = servicesSuppliedRepository;
            _converterHelper = converterHelper;
            _dealershipRepository = dealershipRepository;
            _scheduleDetailRepository = scheduleDetailRepository;
            _serviceRepository = serviceRepository;
        }



        public async Task<IActionResult> Index()
        {

            var userName = this.User.Identity.Name;

            var user = await _userHelper.GetUserByEmailAsync(userName);


            if (user.IsActive == false)
            {
                return RedirectToAction("EditUser", "Accounts");
            }

            var ScheduleDetail = _scheduleDetailRepository.GetSchedulesDetail(user.Id);



            return View(ScheduleDetail);
        }


        public async Task<IActionResult> BeginNew()
        {


            var userName = this.User.Identity.Name;

            var user = await _userHelper.GetUserByEmailAsync(userName);


            if (user.IsActive == false)
            {
                return RedirectToAction("EditUser", "Accounts");
            }

            var services = _servicesSuppliedRepository.GetServices();


            var vehicles = _vehicleRepository.GetUserVehicles(user.Id);



            var model = _converterHelper.ToNewScheduleViewModel(vehicles, services);
            if (vehicles.ToList().Count == 0)
            {

                ViewBag.NoVehicles = "No cars, send back";
                return View(model);
            }

            return View(model);

        }




        [HttpPost]
        public async Task<IActionResult> BeginNew(BeginScheduleViewModel model)
        {

            if (ModelState.IsValid)
            {
                var service = await _servicesSuppliedRepository.GetService(model.ServicesSuppliedId);

                if (!await _scheduleDetailRepository.IsDuplicatedSchedulesForSameServiceAsync(model.VehicleId, model.ServicesSuppliedId))
                {
                    return RedirectToAction("CompleteSchedule", model);
                }
                else
                {
                    var services = _servicesSuppliedRepository.GetServices();

                    var userName = this.User.Identity.Name;

                    var user = await _userHelper.GetUserByEmailAsync(userName);

                    var vehicles = _vehicleRepository.GetUserVehicles(user.Id);
                    var returnModel = _converterHelper.ToNewScheduleViewModel(vehicles, services);
                    ViewBag.ServiceDuplicated = $"There is already a schedule registered for {service.Service.ServiceType}, please chose another or change the current schedule";
                    return View(returnModel);
                }

            }

            return View(model);
        }


        public async Task<IActionResult> CompleteSchedule(BeginScheduleViewModel model)
        {

            var service = await _servicesSuppliedRepository.GetService(model.ServicesSuppliedId);
            var newModel = _converterHelper.ToCompleteScheduleViewModel(model, service.Service);
            var list = await GetDisabledDays(model.ServicesSuppliedId, model.DealershipId);
            var days = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            newModel.DaysToDisable = days;

            return View(newModel);
        }

        [HttpPost]
        public async Task<ActionResult> CompleteSchedule(CompleteScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {

                var service = await _serviceRepository.GetByIdAsync(model.ServiceId);
                var activeSchedule = _converterHelper.ToActiveSchedule(model, service);
                var vehicle = await _vehicleRepository.GetByIdAsync(model.VehicleId);
                var dealership = await _dealershipRepository.GetByIdAsync(model.DealershipId);

                var scheduleDetail = _converterHelper.ToScheduleDetail(vehicle, activeSchedule, dealership);


                try
                {
                    await _scheduleDetailRepository.CreateAsync(scheduleDetail);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There is already a schedule for that service");
                        return View(model);
                    }

                }




            }
            return View(model);
        }


        public async Task<JsonResult> GetDealershipByService(int serviceId)
        {
            var dealership = await _servicesSuppliedRepository.GetDealershipsByServicesasync(serviceId);


            return this.Json(dealership);

        }

        /// <summary>
        /// This method checks if the number of schedules by service per a specific dealership is full and disables dates in View
        /// </summary>
        /// <param name="servicesSuppliedId"></param>
        /// <param name="dealershipId"></param>
        /// <returns>a List of dates to be disabled</returns>
        public async Task<List<DateTime>> GetDisabledDays(int servicesSuppliedId, int dealershipId)
        {
            var list = await _activeScheduleRepository.GetDaysByServiceId(servicesSuppliedId);

            List<DateTime> days = new List<DateTime>();

            var services = await _servicesSuppliedRepository.GetDealershipServicesPerDayAsync(servicesSuppliedId, dealershipId);

            if (list.Count > 0)
            {
                int count = 1;
                for (int i = 1; i < list.Count; i++)
                {
                    if (list[i].ScheduleDay == list[i - 1].ScheduleDay)
                    {
                        count += 1;
                        if (count == services.ServicesPerDay)
                        {
                            days.Add(list[i].ScheduleDay);
                            count = 1;
                        }
                    }
                }

                return days;
            }

            return days;

        }


        public async Task<IActionResult> EditSchedule(int? id)
        {


            var userName = this.User.Identity.Name;

            var user = await _userHelper.GetUserByEmailAsync(userName);


            if (user.IsActive == false)
            {
                return RedirectToAction("EditUser", "Accounts");
            }

            if (id == null)
            {
                return NotFound();
            }

            var activeScheduleId = id.Value;

            var result = await _activeScheduleRepository.ExistsAsync(activeScheduleId);

            if (!result)
            {
                return View();
            }

            var scheduleDetail = await _scheduleDetailRepository.GetScheduleDetailAsync(activeScheduleId);




            var list = await GetDisabledDays(scheduleDetail.ActiveSchedule.Services.Id, scheduleDetail.Dealership.Id);

            var services = _servicesSuppliedRepository.GetWithServicesByDealershipId(scheduleDetail.Dealership.Id);

            var model = _converterHelper.ToEditScheduleViewModel(scheduleDetail, services);

            var days = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            model.DaysToDisable = days;
            model.ActiveScheduleId = activeScheduleId;

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditSchedule(EditScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var activeSchedule = await _converterHelper.ToActiveScheduleFromEditAsync(model);


                try
                {
                    await _activeScheduleRepository.UpdateAsync(activeSchedule);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(model);
                }
            }

            return View(model);
        }


        public async Task<IActionResult> DetailsSchedule(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var scheduleDetail = await _scheduleDetailRepository.GetScheduleDetailAsync(id.Value);

            var model = await _converterHelper.ToScheduleDetailsViewModelAsync(scheduleDetail);

            return View(model);
        }



        public async Task<IActionResult> DeleteSchedule(int? Id)
        {

            var userName = this.User.Identity.Name;

            var user = await _userHelper.GetUserByEmailAsync(userName);


            if (user.IsActive == false)
            {
                return RedirectToAction("EditUser", "Accounts");
            }

            if (Id == null)
            {
                return NotFound();
            }

            var scheduleDetail = await _scheduleDetailRepository.GetScheduleDetailByIdAsync(Id.Value);


            var model = _converterHelper.ToDeleteScheduleViewModel(scheduleDetail);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSchedule(DeleteScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {

                var activeSchedule = await _activeScheduleRepository.GetByIdAsync(model.ActiveScheduleId);

                var scheduleDetail = await _scheduleDetailRepository.GetScheduleDetailAsync(model.ActiveScheduleId);

                if (activeSchedule == null)
                {
                    return View(model);
                }

                try
                {
                    await _scheduleDetailRepository.DeleteAsync(scheduleDetail);
                    await _activeScheduleRepository.DeleteAsync(activeSchedule);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    throw;
                }

            }

            return View(model);
        }
    }
}
