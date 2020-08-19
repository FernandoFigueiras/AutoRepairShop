using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Controllers.BackOffice
{
    public class ColorsController : Controller
    {
        private readonly IColorRepository _colorRepository;

        public ColorsController(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        // GET: Colors
        public IActionResult Index()
        {
            return View(_colorRepository.GetAll());
        }

        // GET: Colors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _colorRepository
                .GetByIdAsync(id.Value);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: Colors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Color color)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    color.IsActive = true;
                    color.CreationDate = DateTime.UtcNow;
                    await _colorRepository.CreateAsync(color);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        if (ModelState.IsValid)
                        {
                            ModelState.AddModelError(String.Empty, $"There is allready a color {color.ColorName} registered, please insert another");
                            return View(color);
                        }

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        return View(color);
                    }
                }

                
            }
            return View(color);
        }

        // GET: Colors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _colorRepository
                .GetByIdAsync(id.Value);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        // POST: Colors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Color color)
        {


            if (ModelState.IsValid)
            {
                try
                {

                    try
                    {
                        await _colorRepository
                      .UpdateAsync(color);
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.Message.Contains("duplicate"))
                        {

                            if (ModelState.IsValid)
                            {
                                ModelState.AddModelError(String.Empty, $"There is already a color {color.ColorName} registered, please insert another");
                                return View(color);
                            }

                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                            return View(color);
                        }
                    }
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _colorRepository.ExistsAsync(color.Id))
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
            return View(color);
        }

        // GET: Colors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await _colorRepository
                .GetByIdAsync(id.Value);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var color = await _colorRepository
                .GetByIdAsync(id);
            await _colorRepository
                .DeleteAsync(color);
            return RedirectToAction(nameof(Index));
        }


    }
}
