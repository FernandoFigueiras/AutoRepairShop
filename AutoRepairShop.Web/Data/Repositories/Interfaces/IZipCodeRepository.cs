using AutoRepairShop.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IZipCodeRepository : IGenericRepository<ZipCode>
    {

        Task<ZipCode> GetZipCodeAsync(string zipCode4, string zipCode3);

        Task<ZipCode> GetZipCodeByIdAsync(int zipCodeId);

        Task<bool> ZipCodeExistsAsync(string zip4, string zip3);

        Task<ZipCode> GetCityIdFromZip4(string zip4);
    }
}
