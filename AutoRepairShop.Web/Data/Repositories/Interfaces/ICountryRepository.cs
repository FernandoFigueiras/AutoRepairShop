using AutoRepairShop.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface ICountryRepository : IGenericRepository<Country>
    {

        IQueryable GetCountriesWithDistricts();

        Task<Country> GetCountryWithDistrictsAsync(int id);
    }
}
