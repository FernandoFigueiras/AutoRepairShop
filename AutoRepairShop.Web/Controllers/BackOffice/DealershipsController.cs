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

namespace AutoRepairShop.Web.Controllers.BackOffice
{
    public class DealershipsController : Controller
    {
        private readonly IDealershipRepository _dealershipRepository;

        public DealershipsController(IDealershipRepository dealershipRepository)
        {
            _dealershipRepository = dealershipRepository;
        }

        // GET: Dealerships
        public IActionResult Index()
        {
            return View(_dealershipRepository.GetAll());
        }

        // GET: Dealerships/Details/5
        public async Task<IActionResult> Details(int? id)
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
                    return RedirectToAction(nameof(Index));
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
    }
}
