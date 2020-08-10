using AutoRepairShop.Web.Data.Entities;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories
{
    public interface IFuelRepository : IGenericRepository<Fuel>
    {


        Task<string> GetFuelNameByIdAsync(int id);


    }
}
