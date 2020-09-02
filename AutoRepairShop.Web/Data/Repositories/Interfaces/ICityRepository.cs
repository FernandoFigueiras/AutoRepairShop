using AutoRepairShop.Web.Data.Entities;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface ICityRepository : IGenericRepository<City>
    {

        Task<bool> ExistsCityInDistrictAsync(int districtId, City city);

        Task<int> GetCityIdByZipCodeAsync(string zipCode4, string zipCode3);



    }

}
