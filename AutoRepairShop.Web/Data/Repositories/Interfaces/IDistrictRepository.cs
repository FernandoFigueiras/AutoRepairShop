using AutoRepairShop.Web.Data.Entities;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IDistrictRepository : IGenericRepository<District>
    {
        Task<bool> ExistsInCountryAsync(int id, string districtName, int districtId);


        Task<District> GetDistrictWithCountiesAsync(int id);
    }
}
