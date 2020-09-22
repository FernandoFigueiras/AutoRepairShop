using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Controllers.BackOffice
{
    public class FuelsController : Controller
    {

        private readonly IFuelRepository _fuelRepository;

        public FuelsController(IFuelRepository fuelRepository)
        {

            _fuelRepository = fuelRepository;
        }

        // GET: Fuels
        public IActionResult Index()
        {
            return View(_fuelRepository.GetAll().OrderBy(f => f.FuelType));
        }

        // GET: Fuels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuel = await _fuelRepository
                .GetByIdAsync(id.Value);
            if (fuel == null)
            {
                return NotFound();
            }

            return View(fuel);
        }

        // GET: Fuels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fuels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Fuel fuel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    fuel.CreationDate = DateTime.UtcNow;
                    fuel.IsActive = true;
                    await _fuelRepository
                        .CreateAsync(fuel);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        if (ModelState.IsValid)
                        {
                            ModelState.AddModelError(string.Empty, $"There is allready a Fuel registered with the name {fuel.FuelType} please insert another");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }

            }
            return View(fuel);
        }

        // GET: Fuels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuel = await _fuelRepository
                .GetByIdAsync(id.Value);
            if (fuel == null)
            {
                return NotFound();
            }
            return View(fuel);
        }

        // POST: Fuels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Fuel fuel)
        {


            if (ModelState.IsValid)
            {
                try
                {

                    try
                    {
                        fuel.UpdateDate = DateTime.UtcNow;
                        if (fuel.IsActive == false)
                        {
                            fuel.DeactivationDate = DateTime.UtcNow;
                        }
                        await _fuelRepository
                            .UpdateAsync(fuel);
                    }
                    catch (Exception ex)
                    {

                        if (ex.InnerException.Message.Contains("duplicate"))
                        {

                            if (ModelState.IsValid)
                            {
                                ModelState.AddModelError(string.Empty, $"There is allready a Fuel registered with the name {fuel.FuelType} please insert another");
                            }
                            return View(fuel);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                            return View(fuel);
                        }
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _fuelRepository.ExistsAsync(fuel.Id))
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
            return View(fuel);
        }

        // GET: Fuels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fuel = await _fuelRepository
                .GetByIdAsync(id.Value);
            if (fuel == null)
            {
                return NotFound();
            }

            return View(fuel);
        }

        // POST: Fuels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fuel = await _fuelRepository
                .GetByIdAsync(id);
            try
            {

                await _fuelRepository
                    .DeleteAsync(fuel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                if (ex.InnerException.Message.Contains("REFERENCE constraint"))
                {

                    if (ModelState.IsValid)
                    {
                        //TODO make buttons to get to the respective views
                        ViewBag.Error = $"There are Vehicles associated with {fuel.FuelType}, so it can not be deleted, please deactivate";
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }
            }

            return View(fuel);
        }
    }
}
