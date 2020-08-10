using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Models;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly DataContext _context;

        public BrandRepository(DataContext context) : base(context)
        {
            _context = context;
        }





        public async Task AddModelAsync(ModelViewModel model)
        {
            var brand = await GetBrandWithModelsAsycn(model.BrandId);

            if (brand == null)
            {
                return;
            }


            brand.Models.Add(new Model { ModelName = model.Name });
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
        }





        public async Task<Brand> GetBrandWithModelsAsycn(int id)
        {
            return await _context.Brands
                .Include(m => m.Models)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }




        /// <summary>
        /// This method returns an INT to retrieve the view to the related brand
        /// </summary>
        /// <param name="model"></param>
        /// <returns>brand ID</returns>
        public async Task<int> UpdateModelAsync(Model model)
        {

            
            if (model == null)
            {
                return 0;
            }
            var brand = await _context.Brands.Where(b => b.Models.Any(m => m.Id == model.Id)).FirstOrDefaultAsync();

            if (brand==null)
            {
                return 0;
            }
            _context.Models.Update(model);
            await _context.SaveChangesAsync();
            return brand.Id;
        }





        public async Task<Model> GetModelByIdAsync(int id)
        {
            return await _context.Models.FindAsync(id);
        }





        /// <summary>
        /// This method returns an INT to retrieve the view to the related brand
        /// </summary>
        /// <param name="model"></param>
        /// <returns>brand ID</returns>
        public async Task<int> DeleteModelAsync(Model model)
        {
            if (model==null)
            {
                return 0;
            }

            var brand = await _context.Brands.Where(b => b.Models.Any(m => m.Id == model.Id)).FirstOrDefaultAsync();
            if (brand==null)
            {
                return 0;
            }

            _context.Models.Remove(model);
            await _context.SaveChangesAsync();
            return brand.Id;
        }





        public async Task<int> GetBrandIdFromModelAsync(int id)
        {

            var brand = await _context.Brands.Where(b => b.Models.Any(m => m.Id == id)).FirstOrDefaultAsync();
            if (brand == null)
            {
                return 0;
            }

            return brand.Id;

        }





        public async Task<bool> ModelNameExistsAsync(string modelName)
        {
            if (modelName == null)
            {
                return false;
            }

            return await _context.Models.AnyAsync(m => m.ModelName == modelName);
        }





        public async Task<string> GetBrandNameByIdAsync(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);

            return brand.BrandName;
        }
        





        public async Task AddModelFromNewVehicleAsync(Model newModel)
        {
            await _context.AddAsync(newModel);
            await _context.SaveChangesAsync();
        }






        public async Task<int> GetModelIdAsync(string modelName)
        {
            var model = await _context.Models.FirstOrDefaultAsync(m =>m.ModelName == modelName);

            return model.Id;
        }




        public async Task<string> GetModelNameByIdAsync(int id)
        {
            var model = await _context.Models.FirstOrDefaultAsync(m => m.Id == id);
            return model.ModelName;
        }

    }
}
