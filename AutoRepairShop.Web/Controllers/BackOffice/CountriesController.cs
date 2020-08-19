using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Helpers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AutoRepairShop.Web.Controllers.BackOffice
{
    public class CountriesController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IDistrictRepository _districtRepository;

        public CountriesController(ICountryRepository countryRepository,
            IConverterHelper converterHelper,
            IDistrictRepository districtRepository)
        {
            _countryRepository = countryRepository;
            _converterHelper = converterHelper;
            _districtRepository = districtRepository;
        }



        public IActionResult Index()
        {
            return View(_countryRepository.GetCountriesWithDistricts());
        }



        public IActionResult CreateCountry()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateCountry(Country country)
        {
            if (ModelState.IsValid)
            {

                country.CreationDate = DateTime.UtcNow;
                country.IsActive = true;

                try
                {
                    await _countryRepository.CreateAsync(country);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(String.Empty, $"There is allready a country registered with the name {country.CountryName} please insert another");
                        return View(country);

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        return View(country);
                    }
                }

            }

            return View(country);
        }



        public async Task<IActionResult> EditCountry(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _countryRepository.GetByIdAsync(id.Value);

            if (country == null)
            {
                return NotFound();
            }

            ViewBag.CountryName = country.CountryName;
            return View(country);
        }






        [HttpPost]
        public async Task<IActionResult> EditCountry(Country country)
        {

            if (ModelState.IsValid)
            {
                if (country == null)
                {
                    ModelState.AddModelError(string.Empty, "The Country does not exist");
                    return View(country);
                }

                try
                {
                    await _countryRepository.UpdateAsync(country);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        ModelState.AddModelError(String.Empty, $"There is allready a country registered with the name {country.CountryName}, please insert another");
                        return View(country);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }

                }
                return RedirectToAction("Index", "Countries");

            }
            return View(country);

        }


        public async Task<IActionResult> DetailsCountry(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _countryRepository.GetCountryWithDistrictsAsync(id.Value);

            if (country == null)
            {
                return NotFound();
            }


            return View(country);
        }


        public async Task<IActionResult> DeleteCountry(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _countryRepository.GetByIdAsync(id.Value);

            if (country == null)
            {
                return NotFound();
            }

            return View(country);

        }


        [HttpPost]
        public async Task<IActionResult> DeleteCountry(int id)
        {


            var country = await _countryRepository.GetByIdAsync(id);

            if (country == null)
            {
                return NotFound();
            }


            try
            {
                await _countryRepository.DeleteAsync(country);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("REFERENCE constraint"))
                {

                    if (ModelState.IsValid)
                    {
                        //TODO make buttons to get to the respective views
                        ViewBag.Error = $"There are districts associated with the country brand named {country.CountryName}, either delete them or deactivate country";
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }
            }


            return View(country);
        }




        /*##############################################   Districts   ####################################################################*/


        public async Task<IActionResult> AddDistrict(int? id)
        {
            var country = await _countryRepository.GetByIdAsync(id.Value);

            if (country==null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToNewDistrictModel(country.Id);

            ViewBag.CountryId = country.Id;
            return View(model);
            
        }



        [HttpPost]
        public async Task<IActionResult> AddDistrict(District district)
        {
            if (ModelState.IsValid)
            {

                var country = await _countryRepository.GetByIdAsync(district.CountryId);

                if (country==null)
                {
                    ModelState.AddModelError(string.Empty, $"The Country {country.CountryName}, is no longer available for ading new districts");
                    return View(district);
                }

                

                var newDistrict = _converterHelper.ToDistrict(district, true);

                var exists = await _districtRepository.ExistsInCountryAsync(country.Id, newDistrict.DistrictName, newDistrict.Id);

                if (exists)
                {
                    ModelState.AddModelError(string.Empty, $"There is allready a District registered with the name {newDistrict.DistrictName}, please insert another");
                    ViewBag.CountryId = district.CountryId;
                    return View(district);
                }

                try
                {
                    await _districtRepository.CreateAsync(newDistrict);


                    return RedirectToAction($"DetailsCountry/{country.Id}");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        ModelState.AddModelError(String.Empty, $"There is allready a District registered with the name {newDistrict.DistrictName}, please insert another");
                        ViewBag.CountryId = district.CountryId;
                        return View(district);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);

                        return View(district);
                    }
                }
                
            }
            ViewBag.CountryId = district.CountryId;
            return View(district);
        }


        public async Task<IActionResult> EditDistrict(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }


            var district = await _districtRepository.GetByIdAsync(id.Value);

            if (district==null)
            {
                return NotFound();
            }

            ViewBag.CountryId = district.CountryId;
            return View(district);
        }




        [HttpPost]
        public async Task<IActionResult> EditDistrict(District district)
        {
            if (ModelState.IsValid)
            {

                var country = await _countryRepository.GetByIdAsync(district.CountryId);
                ViewBag.CountryId = district.CountryId;
                if (country==null)
                {
                    ModelState.AddModelError(string.Empty, $"The country {country.CountryName} is no longer available");
                    return RedirectToAction(nameof(Index));
                }

                var exists = await _districtRepository.ExistsInCountryAsync(district.CountryId, district.DistrictName, district.Id);

                if (exists)
                {
                    ModelState.AddModelError(string.Empty, $"There is already a District named {district.DistrictName} in {country.CountryName}");
                    return View(district);

                }

                var newDistrict = _converterHelper.ToDistrict(district, false);

                try
                {
                    await _districtRepository.UpdateAsync(newDistrict);

                    return RedirectToAction($"DetailsCountry/{district.CountryId}");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        ModelState.AddModelError(String.Empty, $"There is allready a district registered with the name {district.DistrictName}, please insert another");
                        return View(district);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        return View(district);
                    }
                }

            }
            ViewBag.CountryId = district.CountryId;
            return View(district);
        }



        public async Task<IActionResult> DeleteDistrict(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var district = await _districtRepository.GetByIdAsync(id.Value);

            if (district==null)
            {
                return NotFound();
            }



            return View(district);
        }



        [HttpPost]
        public async Task<IActionResult> DeleteDistrict(int id)
        {
            if (ModelState.IsValid)
            {
                var district = await _districtRepository.GetByIdAsync(id);

                try
                {
                    await _districtRepository.DeleteAsync(district);

                    var country = await _countryRepository.GetByIdAsync(district.CountryId);

                    if (country!=null)
                    {
                        return RedirectToAction($"DetailsCountry/{country.Id}");
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("REFERENCE constraint"))
                    {

                        if (ModelState.IsValid)
                        {
                            //TODO make buttons to get to the respective views
                            ViewBag.Error = $"There are counties associated with the District named {district.DistrictName}, either delete them or deactivate country";
                            return View();
                        }

                    }
                    else
                    {
                        ViewBag.Error= ex.InnerException.Message;
                        return View();
                    }
                }

            }

            return View();
        }


        
    }
}
