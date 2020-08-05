using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoRepairShop.Web.Data;
using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories;

namespace AutoRepairShop.Web.Controllers.Backend
{
    public class BrandsController : Controller
    {
        private readonly IBrandRepository _brandRepository;

        public BrandsController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        // GET: Brands
        public IActionResult Index()
        {
            return View(_brandRepository.GetAll().OrderBy(b => b.BrandName));
        }

        // GET: Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandRepository
                .GetByIdAsync(id.Value);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                brand.CreationDate = DateTime.UtcNow;
                brand.IsActive = true;
                await _brandRepository.CreateAsync(brand);
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandRepository
                .GetByIdAsync(id.Value);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Brand brand)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    brand.UpdateDate = DateTime.UtcNow;
                    if (brand.IsActive!=true)
                    {
                        brand.DeactivationDate = DateTime.UtcNow;
                    }
                    await _brandRepository.UpdateAsync(brand);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _brandRepository.ExistsAsync(brand.Id))
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
            return View(brand);
        }

        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandRepository
                .GetByIdAsync(id.Value);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await _brandRepository
                .GetByIdAsync(id);
            await _brandRepository.DeleteAsync(brand);
            return RedirectToAction(nameof(Index));
        }

    }
}
