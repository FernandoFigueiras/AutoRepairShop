using AutoRepairShop.Web.Data;
using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Helpers.Interfaces;
using AutoRepairShop.Web.Models.DShip;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Controllers.BackOffice
{
    public class DealershipsController : Controller
    {
        private readonly IDealershipRepository _dealershipRepository;
        private readonly IServicesSuppliedRepository _servicesSuppliedRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly ICityRepository _cityRepository;
        private readonly IZipCodeRepository _zipCodeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDealershipDepartmentRepository _dealershipDepartmentRepository;
        private readonly IDealershipServiceRepository _dealershipServiceRepository;

        public DealershipsController(IDealershipRepository dealershipRepository,
            IServicesSuppliedRepository servicesSuppliedRepository,
            IServiceRepository serviceRepository,
            IConverterHelper converterHelper,
            ICityRepository cityRepository,
            IZipCodeRepository zipCodeRepository,
            IDepartmentRepository departmentRepository,
            IDealershipDepartmentRepository dealershipDepartmentRepository,
            IDealershipServiceRepository dealershipServiceRepository)
        {
            _dealershipRepository = dealershipRepository;
            _servicesSuppliedRepository = servicesSuppliedRepository;
            _serviceRepository = serviceRepository;
            _converterHelper = converterHelper;
            _cityRepository = cityRepository;
            _zipCodeRepository = zipCodeRepository;
            _departmentRepository = departmentRepository;
            _dealershipDepartmentRepository = dealershipDepartmentRepository;
            _dealershipServiceRepository = dealershipServiceRepository;
        }

        // GET: Dealerships
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_dealershipRepository.GetAllWithZipCode());
        }

        // GET: Dealerships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealership = await _dealershipRepository.GetWithZipCodeAsync(id.Value);


            if (dealership == null)
            {
                return NotFound();
            }

            return View(dealership);
        }

        // GET: Dealerships/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dealerships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dealership dealership)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    dealership.CreationDate = DateTime.Now;
                    dealership.IsActive = true;
                    var zipcode = await _zipCodeRepository.GetByIdAsync(dealership.ZipCodeId);
                    dealership.ZipCode = zipcode;
                    await _dealershipRepository.CreateAsync(dealership);

                    return RedirectToAction($"AddDealershipToServices", dealership);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        ModelState.AddModelError(string.Empty, $"There is allready a Dealership registered with the name {dealership.DealerShipName} please insert another");
                        return View(dealership);
                      

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        return View(dealership);
                    }
                }



            }
            return View(dealership);
        }


        [Authorize(Roles = "Employee/Management, Admin")]
        public async Task<IActionResult> AddDealershipToServices(Dealership dealership)
        {

            var dShip = await _dealershipRepository.GetByNameAsync(dealership.DealerShipName);

            var services = await _serviceRepository.GetAllServicesAsync();

            foreach (var item in services)
            {
                await _servicesSuppliedRepository.AddServicesToDealershipAsync(item, dShip);
            }

            


            return RedirectToAction("AddDepartmentsToDealership", dShip);
        }

        [Authorize(Roles = "Employee/Management, Admin")]
        public async Task<IActionResult> AddDepartmentsToDealership(Dealership dealership)
        {

            var dShip = await _dealershipRepository.GetByNameAsync(dealership.DealerShipName);

            var departments = await _departmentRepository.GetDepartments();

            foreach (var item in departments)
            {
                await _dealershipDepartmentRepository.AddDepartmentToDealershipAsync(item, dShip);
            }




            return RedirectToAction(nameof(Index));
        }


        // GET: Dealerships/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealership = await _dealershipRepository.GetWithZipCodeAsync(id.Value);
            if (dealership == null)
            {
                return NotFound();
            }
            return View(dealership);
        }

        // POST: Dealerships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Dealership dealership)
        {

            if (ModelState.IsValid)
            {
                var dShip = await _dealershipRepository.GetByIdAsync(dealership.Id);

                if (dealership.IsActive == true)
                {
                    try
                    {

                        var services = await _serviceRepository.GetAllServicesAsync();
                        

                        foreach (var item in services)
                        {
                            await _servicesSuppliedRepository.AddServicesToDealershipAsync(item, dShip);
                        }


                        var departments = await _departmentRepository.GetDepartments();

                        foreach (var item in departments)
                        {
                            await _dealershipDepartmentRepository.AddDepartmentToDealershipAsync(item, dShip);
                        }

                        dShip.IsActive = true;
                        dShip.UpdateDate = DateTime.Now;

                       
                        var zipcode = await _zipCodeRepository.GetByIdAsync(dealership.ZipCodeId);
                        dShip.ZipCode = zipcode;
                        await _dealershipRepository.UpdateAsync(dShip);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await _dealershipRepository.ExistsAsync(dealership.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    try
                    {
                        var departments = _dealershipDepartmentRepository.GetDealershipDepartments(dShip.Id);

                        await _dealershipDepartmentRepository.DeleteDEalershipDepartmentsAsync(departments);

                        var services = _servicesSuppliedRepository.GetWithServicesByDealershipId(dShip.Id);

                        await _dealershipServiceRepository.DeleteServicesFromDealershipAsync(services);


                        dShip.IsActive = false;
                        dealership.UpdateDate = DateTime.Now;


                        var zipcode = await _zipCodeRepository.GetByIdAsync(dShip.ZipCodeId);
                        dShip.ZipCode = zipcode;
                        await _dealershipRepository.UpdateAsync(dShip);

                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await _dealershipRepository.ExistsAsync(dealership.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    
                }


               
            }
            return View(dealership);
        }

        // GET: Dealerships/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealership = await _dealershipRepository.GetByIdAsync(id.Value);
            var zipcode = await _zipCodeRepository.GetByIdAsync(dealership.ZipCodeId);
            dealership.ZipCode = zipcode;


            if (dealership == null)
            {
                return NotFound();
            }

            return View(dealership);
        }

        // POST: Dealerships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dealership = await _dealershipRepository.GetByIdAsync(id);
            var departments =  _dealershipDepartmentRepository.GetDealershipDepartments(id);

            await _dealershipDepartmentRepository.DeleteDEalershipDepartmentsAsync(departments);
      
            var services = _servicesSuppliedRepository.GetWithServicesByDealershipId(id);

            await _dealershipServiceRepository.DeleteServicesFromDealershipAsync(services);

            dealership.IsActive = false;
            dealership.DeactivationDate = DateTime.Now;

            await _dealershipRepository.UpdateAsync(dealership);

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddService(int? id)
        {
            var dealership = await _dealershipRepository.GetByIdAsync(id.Value);



            if (dealership == null)
            {
                return NotFound();
            }

            var services = _servicesSuppliedRepository.GetServicesSupplied(dealership.Id).ToList();

            if (services.Count > 0)
            {
                var model = _converterHelper.ToDealershipViewModel(dealership.Id, dealership.DealerShipName, services);

                return View(model);
            }
            else
            {
                
                var model = _converterHelper.ToDealershipViewModel(dealership.Id, dealership.DealerShipName, services);
                ViewData["Noservice"] = "Dealership is not active";
                return View(model);
            }
            
        }



        [HttpPost]
        [Authorize(Roles = "Employee/Management, Admin")]
        public async Task<IActionResult> AddService(DealershipServicesViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Services!=null)
                {
                    foreach (var item in model.Services)
                    {


                        await _servicesSuppliedRepository.UpdateAsync(item);

                    }




                    return RedirectToAction(nameof(Index));
                }

              

            }


            return RedirectToAction("AddService", model.DealershipId);

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
