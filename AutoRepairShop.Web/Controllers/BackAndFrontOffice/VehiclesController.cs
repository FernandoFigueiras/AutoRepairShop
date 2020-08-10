using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories;
using AutoRepairShop.Web.Models;
using AutoRepairShop.Web.Models.VehicleViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairShop.Web.Controllers.BackAndFrontOffice
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IFuelRepository _fuelRepository;
        private readonly IColorRepository _colorRepository;

        public VehiclesController(IVehicleRepository vehicleRepository,
            IBrandRepository brandRepository,
            IFuelRepository fuelRepository,
            IColorRepository colorRepository)
        {
            _vehicleRepository = vehicleRepository;
            _brandRepository = brandRepository;
            _fuelRepository = fuelRepository;
            _colorRepository = colorRepository;
        }


        public IActionResult Index()
        {
            return View(_vehicleRepository.GetVehiclesWithBrandModelFuelAndColor());
        }




        public IActionResult AddVehicle()
        {
            var model = new AddVehicleViewModel
            {
                Brands = _vehicleRepository.GetComboBrands(),
                Models = _vehicleRepository.GetComboModels(),
                Fuels = _vehicleRepository.GetComboFuels(),
                Colors = _vehicleRepository.GetComboColors(),
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddVehicle(AddVehicleViewModel model)
        {

            if (ModelState.IsValid)
            {

                var vehicle = new Vehicle
                {
                    LicencePlate = model.LicencePlate,
                    BrandId = model.BrandId,
                    ModelId = model.ModelId,
                    EngineCapacity = model.EngineCapacity,
                    FuelId = model.FuelId,
                    ColorId = model.ColorId,
                    CreationDate = DateTime.UtcNow,
                };

                try
                {

                    if (model.ModelName != null)
                    {
                        if (await _brandRepository.ModelNameExistsAsync(model.ModelName))
                        {
                            ModelState.AddModelError(string.Empty, $"There is already a model registered with {model.ModelName}, please chose from list of models");
                            model.Brands = await _vehicleRepository.GetComboSoloBrand(model).ToAsyncEnumerable().ToList();
                            model.Models = _vehicleRepository.GetComboModels();
                            model.Fuels = _vehicleRepository.GetComboFuels();
                            model.Colors = _vehicleRepository.GetComboColors();
                            return View(model);
                        }

                        if (await _brandRepository.ExistsAsync(model.BrandId))
                        {
                            var newModel = new Model
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

                                    ModelState.AddModelError(String.Empty, $"There is already a model registered with the name {newModel.ModelName}, please chose another from the list");
                                    model.Brands = await _vehicleRepository.GetComboSoloBrand(model).ToAsyncEnumerable().ToList();
                                    model.Models = _vehicleRepository.GetComboModels();
                                    model.Fuels = _vehicleRepository.GetComboFuels();
                                    model.Colors = _vehicleRepository.GetComboColors();
                                    return View(model);

                                }
                                else
                                {
                                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                                    model.Brands = await _vehicleRepository.GetComboSoloBrand(model).ToAsyncEnumerable().ToList();
                                    model.Models = _vehicleRepository.GetComboModels();
                                    model.Fuels = _vehicleRepository.GetComboFuels();
                                    model.Colors = _vehicleRepository.GetComboColors();
                                    return View(model);
                                }
                            }

                            var modelId = await _brandRepository.GetModelIdAsync(model.ModelName);

                            var vehicle1 = new Vehicle
                            {
                                LicencePlate = model.LicencePlate,
                                BrandId = model.BrandId,
                                ModelId = modelId,
                                EngineCapacity = model.EngineCapacity,
                                FuelId = model.FuelId,
                                Color = model.Color
                            };

                            try
                            {
                                await _vehicleRepository.CreateAsync(vehicle1);
                                return RedirectToAction(nameof(Index));
                            }
                            catch (Exception ex)
                            {
                                if (ex.InnerException.Message.Contains("duplicate"))
                                {


                                    ModelState.AddModelError(String.Empty, $"There is already a Car registered with the Licence PLate number {vehicle.LicencePlate}");
                                    model.Brands = _vehicleRepository.GetComboBrands();
                                    model.Models = _vehicleRepository.GetComboModels();
                                    model.Fuels = _vehicleRepository.GetComboFuels();
                                    model.Colors = _vehicleRepository.GetComboColors();
                                    return View(model);
                                }
                                else
                                {
                                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                                    model.Brands = _vehicleRepository.GetComboBrands();
                                    model.Models = _vehicleRepository.GetComboModels();
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
                            model.Models = _vehicleRepository.GetComboModels();
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
                            ModelState.AddModelError(String.Empty, $"There is already a Car registered with the licence Plate {vehicle}, please insert another");
                            model.Brands = _vehicleRepository.GetComboBrands();
                            model.Models = _vehicleRepository.GetComboModels();
                            model.Fuels = _vehicleRepository.GetComboFuels();
                            model.Colors = _vehicleRepository.GetComboColors();
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                        model.Brands = _vehicleRepository.GetComboBrands();
                        model.Models = _vehicleRepository.GetComboModels();
                        model.Fuels = _vehicleRepository.GetComboFuels();
                        model.Colors = _vehicleRepository.GetComboColors();
                        return View(model);
                    }
                }
            }
            return View(model);
        }




        public async Task<IActionResult> DeleteVehicle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);
            var brand = await _brandRepository.GetByIdAsync(vehicle.BrandId);
            var model = await _brandRepository.GetModelByIdAsync(vehicle.ModelId);
            var fuel = await _fuelRepository.GetByIdAsync(vehicle.FuelId);


            if (vehicle == null)
            {
                return NotFound();
            }




            var modelView = new DeleteVehicleViewModel
            {
                LicencePlate = vehicle.LicencePlate,
                Brand = brand.BrandName,
                ModelName = model.ModelName,
                EngineCapacity = vehicle.EngineCapacity,
                FuelType = fuel.FuelType,
                Color = vehicle.Color
            };

            return View(modelView);
        }



        //TODO CHECK IF ANY schedule is set for this car
        [HttpPost]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await  _vehicleRepository.GetByIdAsync(id);

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
                        ViewBag.Error = $"Could not delete the vehicle with the licence plate {vehicle.LicencePlate}, please try again later";
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }






        public async Task<IActionResult> EditVehicle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);
            var brand = await _brandRepository.GetByIdAsync(vehicle.BrandId);
            var modelType = await _brandRepository.GetModelByIdAsync(vehicle.ModelId);


            if (vehicle == null)
            {
                return NotFound();
            }

            var model = new EditVehicleViewModel
            {
                LicencePlate = vehicle.LicencePlate,
                BrandName = brand.BrandName,
                ModelName = modelType.ModelName,
                EngineCapacity = vehicle.EngineCapacity,
                BrandId = brand.Id,
                Models = _vehicleRepository.GetComboModels(),
                ModelId = modelType.Id,
                FuelId = vehicle.FuelId,
                Fuels = _vehicleRepository.GetComboFuels(),
                ColorId = vehicle.ColorId,
                Colors = _vehicleRepository.GetComboColors(),

        };


            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> EditVehicle(EditVehicleViewModel model)
        {
            if (ModelState.IsValid)
            {

                var vehicle = new Vehicle
                {
                    Id = model.Id,
                    LicencePlate = model.LicencePlate,
                    BrandId = model.BrandId,
                    ModelId = model.ModelId,
                    EngineCapacity = model.EngineCapacity,
                    FuelId = model.FuelId,
                    ColorId = model.ColorId,
                    UpdateDate = DateTime.UtcNow,
                };


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
                            ModelState.AddModelError(String.Empty, $"There is already a vehicle registered with the licence plate {vehicle.LicencePlate}, the change can not me made");
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



        public async Task<IActionResult> DetailsVehicle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);
            var brand = await _brandRepository.GetByIdAsync(vehicle.BrandId);
            var modelName = await _brandRepository.GetModelByIdAsync(vehicle.ModelId);
            var fuel = await _fuelRepository.GetByIdAsync(vehicle.FuelId);
            var color = await _colorRepository.GetByIdAsync(vehicle.ColorId);


            var model = new VehicleDetailsViewModel
            {
                Id = vehicle.Id,
                LicencePlate = vehicle.LicencePlate,
                BrandName = brand.BrandName,
                ModelName = modelName.ModelName,
                EngineCapacity = vehicle.EngineCapacity,
                Fueltype = fuel.FuelType,
                ColorName = color.ColorName,
            };


            if (vehicle == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
