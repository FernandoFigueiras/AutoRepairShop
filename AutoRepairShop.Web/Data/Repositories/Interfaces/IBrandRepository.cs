using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Models;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task AddModelAsync(ModelViewModel model);




        Task AddModelFromNewVehicleAsync(Model newModel);




        Task<Brand> GetBrandWithModelsAsycn(int id);




        Task<Model> GetModelByIdAsync(int id);




        Task<int> GetModelIdAsync(string modelName);




        Task<int> UpdateModelAsync(Model model);




        Task<int> DeleteModelAsync(Model model);




        Task<int> GetBrandIdFromModelAsync(int id);




        Task<bool> ModelNameExistsAsync(string modelName);




        Task<string> GetBrandNameByIdAsync(int id);




        Task<string> GetModelNameByIdAsync(int id);



        IEnumerable<Model> GetModelsFromBrand(int brandId);
    }
}
