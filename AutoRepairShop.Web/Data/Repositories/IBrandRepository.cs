using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Models;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        Task AddModelAsync(ModelViewModel model);


        Task<Brand> GetBrandWithModelsAsycn(int id);


        Task<Model> GetModelByIdAsync(int id);


        Task<int> UpdateModelAsync(Model model);


        Task<int> DeleteModelAsync(Model model);


        Task<int> GetBrandIdFromModelAsync(int id);
    }
}
