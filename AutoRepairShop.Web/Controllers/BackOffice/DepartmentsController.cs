using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Controllers.BackOffice
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {

            var department = _departmentRepository.GetAll();

            return View(department);
        }



        public IActionResult CreateDepartment()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    department.CreationDate = DateTime.Now;
                    department.IsActive = true;
                    await _departmentRepository.CreateAsync(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {


                        ModelState.AddModelError(string.Empty, $"There is allready a Department registered with the name {department.DepartmentName}, please insert another");
                        return View(department);


                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        return View(department);
                    }

                }
            }

            return View(department);
        }


        public async Task<IActionResult> EditDepartment(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var department = await _departmentRepository.GetByIdAsync(Id.Value);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }



        [HttpPost]
        public async Task<IActionResult> EditDepartment(Department department)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await _departmentRepository.UpdateAsync(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {


                        ModelState.AddModelError(String.Empty, $"There is allready a Department registered with the name {department.DepartmentName}.");
                        return View(department);


                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        return View(department);
                    }
                }
            }
            return View(department);



        }


        public async Task<IActionResult> DetailsDepartment(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var department = await _departmentRepository.GetByIdAsync(Id.Value);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }


        public async Task<IActionResult> DeleteDepartment(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var department = await _departmentRepository.GetByIdAsync(Id.Value);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteDepartment(int Id)
        {

            var department = await _departmentRepository.GetByIdAsync(Id);

            if (department == null)
            {
                return NotFound();
            }
            try
            {
                await _departmentRepository.DeleteAsync(department);

            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    ViewBag.Error = $"There are dealerships that suport this {department.DepartmentName}, deactivate the department";
                    return View(department);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    return View(department);
                }
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
