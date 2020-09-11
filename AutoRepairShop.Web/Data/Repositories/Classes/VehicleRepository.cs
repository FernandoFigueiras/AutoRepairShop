using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Models.VehicleViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        private readonly DataContext _context;

        public VehicleRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public IEnumerable<Vehicle> GetUserVehicles(string userId)
        {
            var result = _context.Vehicles.Include(b => b.Model).ThenInclude(b => b.Brand).Include(v => v.Fuel).Include(v => v.Color).Where(v => v.User.Id == userId);
            return result;

        }





        public IEnumerable<SelectListItem> GetComboBrands()
        {
            var list = _context.Brands.Select(b => new SelectListItem
            {
                Text = b.BrandName,
                Value = b.Id.ToString()
            }).ToList();

            if (list.Count > 1)
            {
                list.Insert(0, new SelectListItem
                {
                    Text = "Select a Brand...",
                    Value = "0"
                });
            }


            return list;
        }



        public async Task<SelectListItem> GetComboSoloBrand(AddVehicleViewModel model)
        {

            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == model.BrandId);

            var list = new SelectListItem
            {
                Text = brand.BrandName,
                Value = brand.Id.ToString(),
            };

            return list;
        }


        public IEnumerable<SelectListItem> GetComboModels(int brandId)
        {



            var temp = _context.Models.AsEnumerable().Where(m => m.BrandId == brandId).ToList();

            var list = temp.Select(m => new SelectListItem
            {
                Text = m.ModelName,
                Value = m.Id.ToString()

            }).ToList();




            list.Insert(0, new SelectListItem
            {
                Text = "Select a Model...",
                Value = "0"
            });

            if (list.Count > 1)
            {
                list.Insert(list.Count(), new SelectListItem
                {
                    Text = "(Other not listed)",
                    Value = (_context.Models.Last().Id + 1).ToString()
                });
            }


            return list;
        }



        public IEnumerable<SelectListItem> GetComboFuels()
        {
            var list = _context.Fuels.Select(f => new SelectListItem
            {
                Text = f.FuelType,
                Value = f.Id.ToString()
            }).ToList();

            if (list.Count > 1)
            {
                list.Insert(0, new SelectListItem
                {
                    Text = "Select a Fuel...",
                    Value = "0"
                });
            }


            return list;
        }

        public IEnumerable<SelectListItem> GetComboColors()
        {
            var list = _context.Colors.Select(c => new SelectListItem
            {
                Text = c.ColorName,
                Value = c.Id.ToString(),
            }).ToList();

            if (list.Count > 1)
            {
                list.Insert(0, new SelectListItem
                {
                    Text = "Select a color...",
                    Value = "0",
                });
            }


            return list;
        }




    }
}
