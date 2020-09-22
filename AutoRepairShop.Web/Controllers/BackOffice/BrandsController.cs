using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Models.ModelBrand;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Controllers.BackOffice
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
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var brand = await _brandRepository.GetBrandWithModelsAsycn(Id.Value);

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
                try
                {
                    brand.CreationDate = DateTime.UtcNow;
                    brand.IsActive = true;
                    await _brandRepository.CreateAsync(brand);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {


                        ModelState.AddModelError(string.Empty, $"There is allready a brand registered with the name {brand.BrandName}, please insert another");
                        return View(brand);


                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        return View(brand);
                    }

                }
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

                    try
                    {
                        brand.UpdateDate = DateTime.UtcNow;
                        if (brand.IsActive == false)
                        {
                            brand.DeactivationDate = DateTime.UtcNow;
                        }
                        await _brandRepository.UpdateAsync(brand);
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.Message.Contains("duplicate"))
                        {

                            if (ModelState.IsValid)
                            {
                                ModelState.AddModelError(string.Empty, $"There is allready a brand registered with the name {brand.BrandName}, please insert another");
                            }
                            return View(brand);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                            return View(brand);
                        }
                    }

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
            try
            {

                await _brandRepository.DeleteAsync(brand);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                if (ex.InnerException.Message.Contains("REFERENCE constraint"))
                {

               
                    ViewBag.Error = $"There are models associated with brand named {brand.BrandName}, either delete them or deactivate brand";
                    return View(brand);

                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }
            }
            return View(brand);
        }




        /*#############################################################    MODEL   #######################################################################*/





        public async Task<IActionResult> AddModel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandRepository.GetByIdAsync(id.Value);

            if (brand == null)
            {
                return NotFound();
            }

            var model = new ModelBrandViewModel
            {
                BrandId = brand.Id,
            };
            ViewBag.Id = model.BrandId;
            return View(model);
        }





        [HttpPost]
        public async Task<IActionResult> AddModel(ModelBrandViewModel model)
        {

            if (this.ModelState.IsValid)
            {

                try
                {
                    await _brandRepository.AddModelAsync(model);

                    var brandId = model.BrandId;

                    return RedirectToAction($"Details/{brandId}");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        if (ModelState.IsValid)
                        {
                            ModelState.AddModelError(string.Empty, $"There is allready a Model registered with the name {model.Name} please insert another");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }

            }

            ViewBag.Id = model.BrandId;
            return this.View(model);
        }





        public async Task<IActionResult> EditModel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandModel = await _brandRepository.GetModelByIdAsync(id.Value);

            var brandId = await _brandRepository.GetBrandIdFromModelAsync(id.Value);

            if (brandModel == null)
            {
                return NotFound();
            }

            ViewBag.Id = brandId;
            return View(brandModel);

        }





        [HttpPost]
        public async Task<IActionResult> EditModel(BrandModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var modelId = await _brandRepository.UpdateModelAsync(model);

                    if (modelId != 0)
                    {
                        return this.RedirectToAction($"Details/{modelId}");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        if (ModelState.IsValid)
                        {
                            ModelState.AddModelError(string.Empty, $"There is allready a Model registered with the name {model.ModelName} please insert another");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }

                }

            }

            var brandIdvb = await _brandRepository.GetBrandIdFromModelAsync(model.Id);

            ViewBag.Id = brandIdvb;
            return this.View(model);

        }






        public async Task<IActionResult> DeleteModel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _brandRepository.GetModelByIdAsync(id.Value);

            if (model == null)
            {
                return NotFound();
            }

            var brandId = await _brandRepository.GetBrandIdFromModelAsync(id.Value);

            ViewBag.Id = brandId;

            return View(model);
        }





        [HttpPost, ActionName("DeleteModel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteModel(int id)
        {

            var model = await _brandRepository.GetModelByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var brandId = await _brandRepository.DeleteModelAsync(model);
            if (brandId != 0)
            {
                return this.RedirectToAction($"Details/{brandId}");
            }
            return this.View(model);
        }


    }
}
