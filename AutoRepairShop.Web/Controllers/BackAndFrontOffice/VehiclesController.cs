using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Helpers.Interfaces;
using AutoRepairShop.Web.Models.VehicleViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Controllers.BackAndFrontOffice
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IFuelRepository _fuelRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;

        public VehiclesController(IVehicleRepository vehicleRepository,
            IBrandRepository brandRepository,
            IFuelRepository fuelRepository,
            IColorRepository colorRepository,
            IConverterHelper converterHelper,
            IUserHelper userHelper,
            IMainWindowConverterHelper mainWindowConverterHelper
            )
        {
            _vehicleRepository = vehicleRepository;
            _brandRepository = brandRepository;
            _fuelRepository = fuelRepository;
            _colorRepository = colorRepository;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Index()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);


            if (user.IsActive == false)
            {
                return RedirectToAction("EditUser", "Accounts");
            }
            var vehicles = _vehicleRepository.GetUserVehicles(user.Id);

            return View(vehicles);

        }



        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddVehicle()
        {

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);


            if (user.IsActive == false)
            {
                return RedirectToAction("EditUser", "Accounts");
            }

            var model = new AddVehicleViewModel
            {
                Brands = _vehicleRepository.GetComboBrands(),
                Fuels = _vehicleRepository.GetComboFuels(),
                Colors = _vehicleRepository.GetComboColors(),
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVehicle(AddVehicleViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                var vehicle = _converterHelper.ToNewVehicle(model, user);


                try
                {

                    if (model.ModelName != null)
                    {
                        if (await _brandRepository.ModelNameExistsAsync(model.ModelName))
                        {
                            ModelState.AddModelError(string.Empty, $"There is already a model registered with {model.ModelName}, please chose from list of models");
                            model.Brands = await _vehicleRepository.GetComboSoloBrand(model).ToAsyncEnumerable().ToList();
                            model.Models = _vehicleRepository.GetComboModels(model.BrandId);
                            model.Fuels = _vehicleRepository.GetComboFuels();
                            model.Colors = _vehicleRepository.GetComboColors();
                            return View(model);
                        }

                        if (await _brandRepository.ExistsAsync(model.BrandId))
                        {
                            var newModel = new BrandModel
                            {
                                ModelName = model.ModelName,
                                BrandId = model.BrandId,
                            };

                            try
                            {
                                await _brandRepository.AddModelFromNewVehicleAsync(newModel);
                            }
                            catch (Exception ex)
                            {
                                if (ex.InnerException.Message.Contains("duplicate"))
                                {

                                    ModelState.AddModelError(string.Empty, $"There is already a model registered with the name {newModel.ModelName}, please chose another from the list");
                                    model.Brands = await _vehicleRepository.GetComboSoloBrand(model).ToAsyncEnumerable().ToList();
                                    model.Models = _vehicleRepository.GetComboModels(model.BrandId);
                                    model.Fuels = _vehicleRepository.GetComboFuels();
                                    model.Colors = _vehicleRepository.GetComboColors();
                                    return View(model);

                                }
                                else
                                {
                                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                                    model.Brands = await _vehicleRepository.GetComboSoloBrand(model).ToAsyncEnumerable().ToList();
                                    model.Models = _vehicleRepository.GetComboModels(model.BrandId);
                                    model.Fuels = _vehicleRepository.GetComboFuels();
                                    model.Colors = _vehicleRepository.GetComboColors();
                                    return View(model);
                                }
                            }

                            var modelId = await _brandRepository.GetModelIdAsync(model.ModelName);
                            model.ModelId = modelId;
                            var vehicle1 = _converterHelper.ToNewVehicle(model, user);

                            try
                            {
                                await _vehicleRepository.CreateAsync(vehicle1);
                                return RedirectToAction(nameof(Index));
                            }
                            catch (Exception ex)
                            {
                                if (ex.InnerException.Message.Contains("duplicate"))
                                {


                                    ModelState.AddModelError(string.Empty, $"There is already a Car registered with the Licence PLate number {vehicle.LicencePlate}");
                                    model.Brands = _vehicleRepository.GetComboBrands();
                                    model.Models = _vehicleRepository.GetComboModels(model.BrandId);
                                    model.Fuels = _vehicleRepository.GetComboFuels();
                                    model.Colors = _vehicleRepository.GetComboColors();
                                    return View(model);
                                }
                                else
                                {
                                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                                    model.Brands = _vehicleRepository.GetComboBrands();
                                    model.Models = _vehicleRepository.GetComboModels(model.BrandId);
                                    model.Fuels = _vehicleRepository.GetComboFuels();
                                    model.Colors = _vehicleRepository.GetComboColors();
                                    return View(model);
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "The Brand chosen is no longer available, please contact the company for more information");
                            model.Brands = _vehicleRepository.GetComboBrands();
                            model.Models = _vehicleRepository.GetComboModels(model.BrandId);
                            model.Fuels = _vehicleRepository.GetComboFuels();
                            model.Colors = _vehicleRepository.GetComboColors();
                            return View(model);
                        }

                    }

                    await _vehicleRepository.CreateAsync(vehicle);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        if (ModelState.IsValid)
                        {
                            ModelState.AddModelError(string.Empty, $"There is already a Car registered with the licence Plate {vehicle}, please insert another");
                            model.Brands = _vehicleRepository.GetComboBrands();
                            model.Models = _vehicleRepository.GetComboModels(model.BrandId);
                            model.Fuels = _vehicleRepository.GetComboFuels();
                            model.Colors = _vehicleRepository.GetComboColors();
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        model.Brands = _vehicleRepository.GetComboBrands();
                        model.Models = _vehicleRepository.GetComboModels(model.BrandId);
                        model.Fuels = _vehicleRepository.GetComboFuels();
                        model.Colors = _vehicleRepository.GetComboColors();
                        return View(model);
                    }
                }
            }
            return View(model);
        }



        [Authorize(Roles = "Employee/Management, Employee/Reception, Admin, Customer")]
        public async Task<IActionResult> DeleteVehicle(int? id)
        {

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);


            if (user.IsActive == false)
            {
                return RedirectToAction("EditUser", "Accounts");
            }

            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);


            if (vehicle == null)
            {
                return NotFound();
            }




            var modelView = await _converterHelper.ToDeleteVehicleViewModelAsync(vehicle);

            return View(modelView);
        }



        [HttpPost]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            try
            {
                await _vehicleRepository.DeleteAsync(vehicle);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("REFERENCE constraint"))
                {

                    if (ModelState.IsValid)
                    {
                        ViewBag.Error = $"Unable to delete the vehicle with the licence plate {vehicle.LicencePlate}, first cancel all  associated schedulles";
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }





        [Authorize(Roles = "Employee/Management, Employee/Reception, Admin, Customer")]
        public async Task<IActionResult> EditVehicle(int? id)
        {

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);


            if (user.IsActive == false)
            {
                return RedirectToAction("EditUser", "Accounts");
            }

            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);


            if (vehicle == null)
            {
                return NotFound();
            }

            var model = await _converterHelper.ToEditVehicleViewModelAsync(vehicle);


            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> EditVehicle(EditVehicleViewModel model)
        {
            if (ModelState.IsValid)
            {

                var vehicle = _converterHelper.ToEditVehicle(model);


                try
                {
                    if (!await _brandRepository.ExistsAsync(vehicle.BrandId))
                    {
                        var brandName = await _brandRepository.GetBrandNameByIdAsync(vehicle.BrandId);

                        ModelState.AddModelError(string.Empty, $"The brand {brandName} is no longer available, please contact the company for more information");
                        return View(model);
                    }

                    if (await _brandRepository.GetModelByIdAsync(vehicle.ModelId) == null)
                    {
                        var modelName = await _brandRepository.GetModelNameByIdAsync(vehicle.ModelId);

                        ModelState.AddModelError(string.Empty, $"The model {modelName} is no longer available, please contact the company for more information");
                        return View(model);
                    }

                    if (!await _fuelRepository.ExistsAsync(vehicle.FuelId))
                    {
                        var fuelType = await _fuelRepository.GetFuelNameByIdAsync(vehicle.FuelId);

                        ModelState.AddModelError(string.Empty, $"The fuel {fuelType} is no longer available, please contact the company for more information");
                        return View(model);
                    }

                    await _vehicleRepository.UpdateAsync(vehicle);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        if (ModelState.IsValid)
                        {
                            ModelState.AddModelError(string.Empty, $"There is already a vehicle registered with the licence plate {vehicle.LicencePlate}, the change can not me made");
                        }
                        return View(model);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        return View(model);
                    }
                }
            }

            return View(model);
        }


        [Authorize(Roles = "Employee/Management, Employee/Reception, Employee/Mechanics, Employee/Electronic,Employee/Paint, Admin, Customer")]
        public async Task<IActionResult> DetailsVehicle(int? id)
        {

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);


            if (user.IsActive == false)
            {
                return RedirectToAction("EditUser", "Accounts");
            }

            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);

            var model = await _converterHelper.ToVehicleDetailsViewModelAsync(vehicle);


            if (vehicle == null)
            {
                return NotFound();
            }

            return View(model);
        }



        public JsonResult GetModelsAsync(int brandId)
        {
            var models = _brandRepository.GetModelsFromBrand(brandId);

            return this.Json(models);

        }



        [Authorize(Roles = "Employee/Management, Employee/Reception, Admin")]
        public IActionResult AddVehicleNoUser()
        {

            var model = new AddVehicleViewModel
            {
                Brands = _vehicleRepository.GetComboBrands(),
                Fuels = _vehicleRepository.GetComboFuels(),
                Colors = _vehicleRepository.GetComboColors(),
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee/Management, Employee/Reception, Admin")]
        public async Task<IActionResult> AddVehicleNoUser(AddVehicleViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync("genericclientuser@autorepairshop.com");
                var vehicle = _converterHelper.ToNewVehicle(model, user);


                try
                {

                    if (model.ModelName != null)
                    {
                        if (await _brandRepository.ModelNameExistsAsync(model.ModelName))
                        {
                            ModelState.AddModelError(string.Empty, $"There is already a model registered with {model.ModelName}, please chose from list of models");
                            model.Brands = await _vehicleRepository.GetComboSoloBrand(model).ToAsyncEnumerable().ToList();
                            model.Models = _vehicleRepository.GetComboModels(model.BrandId);
                            model.Fuels = _vehicleRepository.GetComboFuels();
                            model.Colors = _vehicleRepository.GetComboColors();
                            return View(model);
                        }

                        if (await _brandRepository.ExistsAsync(model.BrandId))
                        {
                            var newModel = new BrandModel
                            {
                                ModelName = model.ModelName,
                                BrandId = model.BrandId,
                            };

                            try
                            {
                                await _brandRepository.AddModelFromNewVehicleAsync(newModel);
                            }
                            catch (Exception ex)
                            {
                                if (ex.InnerException.Message.Contains("duplicate"))
                                {

                                    ModelState.AddModelError(string.Empty, $"There is already a model registered with the name {newModel.ModelName}, please chose another from the list");
                                    model.Brands = await _vehicleRepository.GetComboSoloBrand(model).ToAsyncEnumerable().ToList();
                                    model.Models = _vehicleRepository.GetComboModels(model.BrandId);
                                    model.Fuels = _vehicleRepository.GetComboFuels();
                                    model.Colors = _vehicleRepository.GetComboColors();
                                    return View(model);

                                }
                                else
                                {
                                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                                    model.Brands = await _vehicleRepository.GetComboSoloBrand(model).ToAsyncEnumerable().ToList();
                                    model.Models = _vehicleRepository.GetComboModels(model.BrandId);
                                    model.Fuels = _vehicleRepository.GetComboFuels();
                                    model.Colors = _vehicleRepository.GetComboColors();
                                    return View(model);
                                }
                            }

                            var modelId = await _brandRepository.GetModelIdAsync(model.ModelName);
                            model.ModelId = modelId;
                            var vehicle1 = _converterHelper.ToNewVehicle(model, user);

                            try
                            {
                                await _vehicleRepository.CreateAsync(vehicle1);
                                return RedirectToAction(nameof(Index));
                            }
                            catch (Exception ex)
                            {
                                if (ex.InnerException.Message.Contains("duplicate"))
                                {


                                    ModelState.AddModelError(string.Empty, $"There is already a Car registered with the Licence PLate number {vehicle.LicencePlate}");
                                    model.Brands = _vehicleRepository.GetComboBrands();
                                    model.Models = _vehicleRepository.GetComboModels(model.BrandId);
                                    model.Fuels = _vehicleRepository.GetComboFuels();
                                    model.Colors = _vehicleRepository.GetComboColors();
                                    return View(model);
                                }
                                else
                                {
                                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                                    model.Brands = _vehicleRepository.GetComboBrands();
                                    model.Models = _vehicleRepository.GetComboModels(model.BrandId);
                                    model.Fuels = _vehicleRepository.GetComboFuels();
                                    model.Colors = _vehicleRepository.GetComboColors();
                                    return View(model);
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "The Brand chosen is no longer available, please contact the company for more information");
                            model.Brands = _vehicleRepository.GetComboBrands();
                            model.Models = _vehicleRepository.GetComboModels(model.BrandId);
                            model.Fuels = _vehicleRepository.GetComboFuels();
                            model.Colors = _vehicleRepository.GetComboColors();
                            return View(model);
                        }

                    }

                    await _vehicleRepository.CreateAsync(vehicle);
                    return RedirectToAction("ShowScheduleForDealership", "ScheduleDetails");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {

                        if (ModelState.IsValid)
                        {
                            ModelState.AddModelError(string.Empty, $"There is already a Car registered with the licence Plate {vehicle}, please insert another");
                            model.Brands = _vehicleRepository.GetComboBrands();
                            model.Models = _vehicleRepository.GetComboModels(model.BrandId);
                            model.Fuels = _vehicleRepository.GetComboFuels();
                            model.Colors = _vehicleRepository.GetComboColors();
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        model.Brands = _vehicleRepository.GetComboBrands();
                        model.Models = _vehicleRepository.GetComboModels(model.BrandId);
                        model.Fuels = _vehicleRepository.GetComboFuels();
                        model.Colors = _vehicleRepository.GetComboColors();
                        return View(model);
                    }
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Employee/Management, Employee/Reception, Employee/Mechanics, Employee/Electronic, Employee/Paint, Admin")]
        public async Task<IActionResult> ShowDealershipVehicles()
        {

            var user = await _userHelper.GetUserByEmailAsync("genericclientuser@autorepairshop.com");

            var vehicles =  _vehicleRepository.GetUserVehicles(user.Id);


            return View(vehicles);
        }
    }
}
