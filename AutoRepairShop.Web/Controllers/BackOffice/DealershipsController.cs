using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoRepairShop.Web.Data;
using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Internal;
using AutoRepairShop.Web.Models.DShip;
using AutoRepairShop.Web.Helpers.Interfaces;

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

        public DealershipsController(IDealershipRepository dealershipRepository,
            IServicesSuppliedRepository servicesSuppliedRepository,
            IServiceRepository serviceRepository,
            IConverterHelper converterHelper,
            ICityRepository cityRepository,
            IZipCodeRepository zipCodeRepository)
        {
            _dealershipRepository = dealershipRepository;
            _servicesSuppliedRepository = servicesSuppliedRepository;
            _serviceRepository = serviceRepository;
            _converterHelper = converterHelper;
            _cityRepository = cityRepository;
            _zipCodeRepository = zipCodeRepository;
        }

        // GET: Dealerships
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
                    await _dealershipRepository.CreateAsync(dealership);


                    return RedirectToAction("AddDealershipToServices");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        if (ModelState.IsValid)
                        {
                            ModelState.AddModelError(String.Empty, $"There is allready a Dealership registered with the name {dealership.DealerShipName} please insert another");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }

               

            }
            return View(dealership);
        }


        public async Task<IActionResult> AddDealershipToServices(Dealership dealership)
        {

            var dShip = await _dealershipRepository.GetByNameAsync(dealership.DealerShipName);

            var services = _serviceRepository.GetAll().ToList();


            foreach (var item in services)
            {
                var servicesSupplied = _converterHelper.ToNewServicesSupplied(dShip, item);

                await _servicesSuppliedRepository.CreateAsync(servicesSupplied);

            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Dealerships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealership = await _dealershipRepository.GetByIdAsync(id.Value);
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
                try
                {
                    await _dealershipRepository.UpdateAsync(dealership);
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
            return View(dealership);
        }

        // GET: Dealerships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dealership = await _dealershipRepository.GetByIdAsync(id.Value);
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
            await _dealershipRepository.DeleteAsync(dealership);
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> AddService(int? id)
        {
            var dealership = await _dealershipRepository.GetByIdAsync(id.Value);

            

            if (dealership == null)
            {
                return NotFound();
            }

            var services = _servicesSuppliedRepository.GetServices().ToList();



            var model = _converterHelper.ToDealershipViewModel(dealership.Id, dealership.DealerShipName, services);
            
            return View(model);
        }



        //TODO view
        [HttpPost]
        public async Task<IActionResult> AddService(DealershipServicesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dealeship = await _dealershipRepository.GetByIdAsync(model.DealershipId);



                    foreach (var item in model.Services)
                    {
                        var service = await _serviceRepository.GetByIdAsync(item.Id);

                        var ServicesSupplied = _converterHelper.ToServicesSupplied(dealeship, service, item.Id, item.IsActive);

                        await _servicesSuppliedRepository.UpdateAsync(ServicesSupplied);
                    }


                return RedirectToAction(nameof(Index));

            }


            return View(model);

        }


        public async Task<int> GetZipCodeIdasync(string zipCode4, string zipCode3)
        {
            var teste=  await _zipCodeRepository.GetZipCodeIdAsync(zipCode4, zipCode3);

            return teste;
        }






        public async Task<int> GetCityIdAsync(string zipCode4, string zipCode3)
        {
            var cityid = await _cityRepository.GetCityIdByZipCodeAsync(zipCode4, zipCode3);

            return cityid;
        }
    }
}
