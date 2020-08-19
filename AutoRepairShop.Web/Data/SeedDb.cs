using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Helpers.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
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

            if (!_context.Countries.Any())
            {

                AddCountry("Portugal");

                await _context.SaveChangesAsync();
            }


            if (!_context.Districts.Any())
            {
                string DistrictFile = "Districts.txt";



                string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"Data\\TextFiles\\",
                DistrictFile);



                if (File.Exists(path))
                {

                    using (StreamReader sr = new StreamReader(path))
                    {
                        var countryId = await _context.Countries.FirstOrDefaultAsync(c => c.CountryName == "Portugal");

                        string d;
                        while ((d = sr.ReadLine()) != null)
                        {
                            string district = d;

                            AddDistrict(district, countryId.Id);
                            await _context.SaveChangesAsync();
                        }
                        sr.Close();
                    }


                }


                if (!_context.Cities.Any())
                {
                    string CountyFile = "Cities.txt";


                    string pathCounty = Path.Combine(
                    Directory.GetCurrentDirectory(),
                     $"Data\\TextFiles\\",
                     CountyFile);


                    if (File.Exists(pathCounty))
                    {
                        using (StreamReader srCity = new StreamReader(pathCounty))
                        {
                            string c;

                            while ((c = srCity.ReadLine()) != null)
                            {
                                string[] line = new string[2];

                                line = c.Split(';');

                                int districtId = Convert.ToInt32(line[0]);
                                string cityName = line[1];

                                AddCity(cityName, districtId);
                            }
                            srCity.Close();
                        }


                        await _context.SaveChangesAsync();
                    }

                }


                if (!_context.ZipCodes.Any())
                {
                    AddZipCode(256, "0000", "000");
                    await _context.SaveChangesAsync();

                    string zipCodesFile = "ZipCode.txt";


                    string pathZipCodes = Path.Combine(
                    Directory.GetCurrentDirectory(),
                     $"Data\\TextFiles\\",
                     zipCodesFile);


                    if (File.Exists(pathZipCodes))
                    {
                        using (StreamReader srZipCode = new StreamReader(pathZipCodes))
                        {
                            string c;

                            while ((c = srZipCode.ReadLine()) != null)
                            {
                                string[] line = new string[17];

                                line = c.Split(',');

                                int cityId = Convert.ToInt32(line[1]);
                                string zipCode4 = line[14];
                                string zipCode3 = line[15];

                                AddZipCode(cityId, zipCode4, zipCode3);
                            }
                            srZipCode.Close();
                        }


                        await _context.SaveChangesAsync();
                    }

                }

                var user = await _userHelper.GetUserByEmailAsync("fjfigdev@gmail.com");


                if (user == null)
                {
                    user = new User
                    {
                        FirstName = "Fernando",
                        LastName = "Figueiras",
                        UserName = "fjfigdev@gmail.com",
                        Email = "fjfigdev@gmail.com",
                        ZipCodeId = 1,
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


                if (! _context.Services.Any())
                {
                    AddService("Maintenance");
                    AddService("Rims / tires");
                    AddService("Mandatory periodic inspection");
                    AddService("Campaign");
                    AddService("Collision");
                    AddService("Paint");
                    AddService("Spare Parts");


                    await _context.SaveChangesAsync();
                }


                if (!_context.Dealerships.Any())
                {
                    AddDealership("AutorepairShop");

                    await _context.SaveChangesAsync();
                }
            }


        }

        private void AddDealership(string dealershipName)
        {
            var dealership = new Dealership
            {
                IsActive =true,
                CreationDate = DateTime.UtcNow,
                DealerShipName=dealershipName,
                Address = "Rua da Autorepairshop",
                ZipCodeId = 1,
            };

            _context.Dealerships.Add(dealership);
        }

        private void AddService(string serviceType)
        {
            var service = new Service
            {
                IsActive=true,
                CreationDate = DateTime.UtcNow,
                ServiceType = serviceType,
            };

            _context.Services.Add(service);
        }

        private void AddZipCode(int cityId, string zipCode4, string zipCode3)
        {
            var zipCode = new ZipCode
            {
                ZipCode4 = zipCode4,
                ZipCode3 = zipCode3,
                CityId = cityId,
            };

            _context.ZipCodes.Add(zipCode);
        }

        private void AddCountry(string countryName)
        {
            var country = new Country
            {
                IsActive = true,
                CreationDate = DateTime.UtcNow,
                CountryName = countryName,
            };

            _context.Countries.Add(country);
        }



        private void AddCity(string countyName, int districtId)
        {
            var county = new City
            {
                IsActive = true,
                CreationDate = DateTime.UtcNow,
                CityName = countyName,
                DistrictId = districtId,
            };

            _context.Cities.Add(county);
        }

        private void AddDistrict(string districtName, int countryId)
        {

            var district = new District
            {
                IsActive = true,
                CreationDate = DateTime.UtcNow,
                DistrictName = districtName,
                CountryId = countryId,

            };

            _context.Districts.Add(district);
        }
    }
}



