using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Helpers.Interfaces;
using AutoRepairShop.Web.Models.ActiveScheduleViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairShop.Web.Controllers.BackAndFrontOffice
{
    public class ScheduleDetailsController : Controller
    {
        private readonly IActiveScheduleRepository _activeScheduleRepository;
        private readonly IUserHelper _userHelper;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IServicesSuppliedRepository _servicesSuppliedRepository;

        public ScheduleDetailsController(IActiveScheduleRepository activeScheduleRepository,
            IUserHelper userHelper,
            IVehicleRepository vehicleRepository,
            IServicesSuppliedRepository servicesSuppliedRepository)
        {
            _activeScheduleRepository = activeScheduleRepository;
            _userHelper = userHelper;
            _vehicleRepository = vehicleRepository;
            _servicesSuppliedRepository = servicesSuppliedRepository;
        }



        public async Task<IActionResult> Index()
        {

            var userName = this.User.Identity.Name;

            var user = await _userHelper.GetUserByEmailAsync(userName);
            

            var ScheduleDetail = _activeScheduleRepository.GetScheduleDetail(user.Id);

            return View(ScheduleDetail);
        }


        public async Task<IActionResult> Create(int vehicleId)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);


            var services = _servicesSuppliedRepository.GetServices();


            var model = new NewScheduleViewModel();
            model.VehicleId = vehicle.Id;
            model.ServicesSupplied = services;

            return View(model);

        }


        public async Task<JsonResult> GetDealershipByService (int serviceId)
        {
            var dealership = await _servicesSuppliedRepository.GetDealershipsByServicesasync(serviceId);

            return this.Json(dealership);


        }
      
    }
}
