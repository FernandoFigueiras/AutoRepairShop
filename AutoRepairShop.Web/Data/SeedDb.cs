using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context,
            IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }



        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();


            var user = await _userHelper.GetUserByEmailAsync("fjfigdev@gmail.com");
           

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Fernando",
                    LastName = "Figueiras",
                    UserName = "fjfigdev@gmail.com",
                    Email = "fjfigdev@gmail.com",
                };
                
                var result = await _userHelper.AddUserAsync(user, "123456");
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                var resulttoken = await _userHelper.ConfirmEmailAsync(user, token);

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("The User could not be created in seeder");
                }
            }


           

            if (!_context.Brands.Any())
            {


                _context.Brands.Add(new Brand
                {
                    IsActive = true,
                    CreationDate = DateTime.UtcNow,
                    BrandName = "Mercedes-Benz",
                });

                _context.Brands.Add(new Brand
                {
                    IsActive = true,
                    CreationDate = DateTime.UtcNow,
                    BrandName = "BMW",
                });

                _context.Brands.Add(new Brand
                {
                    IsActive = true,
                    CreationDate = DateTime.UtcNow,
                    BrandName = "Porsche",
                });

                _context.Brands.Add(new Brand
                {
                    IsActive = true,
                    CreationDate = DateTime.UtcNow,
                    BrandName = "Aston-Martin",
                });

                await _context.SaveChangesAsync();
  
            }

            if (!_context.Models.Any())
            {
                Brand mercedes = await _context.Brands.FirstOrDefaultAsync(b => b.BrandName == "Mercedes-Benz");

                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "CL65 AMG",
                    BrandId = mercedes.Id,
                });
                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "AMG-GT Roadster",
                    BrandId = mercedes.Id,
                });
                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "SL65 AMG Black Series",
                    BrandId = mercedes.Id,
                });
                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "CLA45 AMG",
                    BrandId = mercedes.Id,
                });
                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "A35 AMG",
                    BrandId = mercedes.Id,
                });


                Brand BMW = await _context.Brands.FirstOrDefaultAsync(b => b.BrandName == "BMW");

                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "M3",
                    BrandId = BMW.Id,
                });
                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "M4",
                    BrandId = BMW.Id,
                });
                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "M5",
                    BrandId = BMW.Id,
                });
                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "M 850i",
                    BrandId = BMW.Id,
                });


                Brand porsche = await _context.Brands.FirstOrDefaultAsync(b => b.BrandName == "Porsche");

                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "Carrera GT3",
                    BrandId = porsche.Id,
                });
                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "911 Turbo",
                    BrandId = porsche.Id,
                });
                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "718 Cayman GT4",
                    BrandId = porsche.Id,
                });
                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "Taycan Turbo S",
                    BrandId = porsche.Id,
                });
                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "Panamera Turbo",
                    BrandId = porsche.Id,
                });


                Brand astonMartin = await _context.Brands.FirstOrDefaultAsync(b => b.BrandName == "Aston-Martin");

                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "Vantage",
                    BrandId = astonMartin.Id,
                });
                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "DB 11",
                    BrandId = astonMartin.Id,
                });
                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "DBS SUPERLEGGERA",
                    BrandId = astonMartin.Id,
                });
                _context.Models.Add(new Model
                {
                    IsActive = true,
                    ModelName = "Vanquish Zagato",
                    BrandId = astonMartin.Id,
                });

                await _context.SaveChangesAsync();

            }


            if (!_context.Fuels.Any())
            {
                _context.Fuels.Add(new Fuel { IsActive = true, CreationDate = DateTime.UtcNow, FuelType = "Petrol" });
                _context.Fuels.Add(new Fuel { IsActive = true, CreationDate = DateTime.UtcNow, FuelType = "Diesel" });
                _context.Fuels.Add(new Fuel { IsActive = true, CreationDate = DateTime.UtcNow, FuelType = "GPL" });
                _context.Fuels.Add(new Fuel { IsActive = true, CreationDate = DateTime.UtcNow, FuelType = "Hybrid" });
                _context.Fuels.Add(new Fuel { IsActive = true, CreationDate = DateTime.UtcNow, FuelType = "Electric" });
                _context.Fuels.Add(new Fuel { IsActive = true, CreationDate = DateTime.UtcNow, FuelType = "Hydrogen" });

                await _context.SaveChangesAsync();
            }


            if (!_context.Colors.Any())
            {
                _context.Colors.Add(new Color { IsActive = true, CreationDate = DateTime.UtcNow, ColorName = "Black" });
                _context.Colors.Add(new Color { IsActive = true, CreationDate = DateTime.UtcNow, ColorName = "White" });
                _context.Colors.Add(new Color { IsActive = true, CreationDate = DateTime.UtcNow, ColorName = "Silver" });
                _context.Colors.Add(new Color { IsActive = true, CreationDate = DateTime.UtcNow, ColorName = "Red" });
                _context.Colors.Add(new Color { IsActive = true, CreationDate = DateTime.UtcNow, ColorName = "Yellow" });
                _context.Colors.Add(new Color { IsActive = true, CreationDate = DateTime.UtcNow, ColorName = "Green" });
                _context.Colors.Add(new Color { IsActive = true, CreationDate = DateTime.UtcNow, ColorName = "Pearl" });
                _context.Colors.Add(new Color { IsActive = true, CreationDate = DateTime.UtcNow, ColorName = "Blue" });

                await _context.SaveChangesAsync();
            }

        }


    }
}
