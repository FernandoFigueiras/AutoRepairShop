using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Helpers.Interfaces;
using AutoRepairShop.Web.Models.DShip;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

                    dealership.CreationDate = DateTime.Now;
                    dealership.IsActive = true;
                    await _dealershipRepository.CreateAsync(dealership);


                    return RedirectToAction("AddDealershipToServices");
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
                    dealership.UpdateDate = DateTime.Now;

                    if (dealership.IsActive == false)
                    {
                        dealership.DeactivationDate = DateTime.Now;
                    }
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
