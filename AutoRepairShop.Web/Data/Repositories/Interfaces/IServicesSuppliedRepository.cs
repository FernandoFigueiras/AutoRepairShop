using AutoRepairShop.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IServicesSuppliedRepository : IGenericRepository<ServicesSupplied>
    {

        IEnumerable<ServicesSupplied> GetWithServices(int id);

        IEnumerable<ServicesSupplied> GetServices();

        Task<ServicesSupplied> GetDealership(int id);
    }
}
