using AutoRepairShop.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IDealershipRepository : IGenericRepository<Dealership>
    {

        Task<Dealership> GetWithZipCodeAsync(int dealershipId);



        IQueryable GetAllWithZipCode();



        Task<Dealership> GetByNameAsync(string dealershipName);
    }
}
