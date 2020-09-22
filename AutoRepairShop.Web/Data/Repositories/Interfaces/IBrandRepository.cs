using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Models;
using AutoRepairShop.Web.Models.ModelBrand;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task AddModelAsync(ModelBrandViewModel model);




        Task AddModelFromNewVehicleAsync(BrandModel newModel);




        Task<Brand> GetBrandWithModelsAsycn(int id);




        Task<BrandModel> GetModelByIdAsync(int id);




        Task<int> GetModelIdAsync(string modelName);




        Task<int> UpdateModelAsync(BrandModel model);




        Task<int> DeleteModelAsync(BrandModel model);




        Task<int> GetBrandIdFromModelAsync(int id);




        Task<bool> ModelNameExistsAsync(string modelName);




        Task<string> GetBrandNameByIdAsync(int id);




        Task<string> GetModelNameByIdAsync(int id);



        IEnumerable<BrandModel> GetModelsFromBrand(int brandId);
    }
}
