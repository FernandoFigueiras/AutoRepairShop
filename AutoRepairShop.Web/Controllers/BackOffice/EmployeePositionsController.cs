using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairShop.Web.Controllers.BackOffice
{
    public class EmployeePositionsController : Controller
    {
        private readonly IEmployeePositionRepository _employeePositionRepository;

        public EmployeePositionsController(IEmployeePositionRepository employeePositionRepository)
        {
            _employeePositionRepository = employeePositionRepository;
        }

        public IActionResult Index()
        {

            var positions = _employeePositionRepository.GetAll();

            return View(positions);
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeePosition position)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    position.CreationDate = DateTime.Now;
                    position.IsActive = true;
                    await _employeePositionRepository.CreateAsync(position);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        if (ModelState.IsValid)
                        {
                            ModelState.AddModelError(string.Empty, $"There is allready a Position registered with the name {position.PositionName} please insert another");
                        }
                        return View(position);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        return View(position);
                    }
                }
            }

            return View(position);
        }


        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var position = await _employeePositionRepository.GetByIdAsync(Id.Value);

            if (position == null)
            {
                return NotFound();
            }

            return View(position);

        }


        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var position = await _employeePositionRepository.GetByIdAsync(Id.Value);


            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EmployeePosition position)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    position.UpdateDate = DateTime.Now;

                    if (position.IsActive == false)
                    {
                        position.DeactivationDate = DateTime.Now;
                    }
                    await _employeePositionRepository.UpdateAsync(position);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        
                        ModelState.AddModelError(string.Empty, $"There is allready a POsition registered with the name {position.PositionName} please insert another");
                      
                        return View(position);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        return View(position);
                    }
                }
            }

            return View(position);
        }



        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var position = await _employeePositionRepository.GetByIdAsync(Id.Value);

            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var position = await _employeePositionRepository.GetByIdAsync(Id);

            if (position == null)
            {
                return NotFound();
            }

            try
            {
                await _employeePositionRepository.DeleteAsync(position);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("REFERENCE constraint"))
                {


                    ViewBag.Error = $"There are Employees associated with the position named {position.PositionName}";
                    return View(position);

                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    return View(position);
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
