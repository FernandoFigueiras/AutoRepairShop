using AutoRepairShop.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IZipCodeRepository : IGenericRepository<ZipCode>
    {

        Task<int> GetZipCodeIdAsync(string zipCode4, string zipCode3);

        Task<ZipCode> GetZipCodeByIdAsync(int zipCodeId);

    }
}
