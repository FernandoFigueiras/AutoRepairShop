using AutoRepairShop.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IServicesSuppliedRepository : IGenericRepository<ServicesSupplied>
    {

        IEnumerable<ServicesSupplied> GetWithServicesByDealershipId(int id);

        IEnumerable<ServicesSupplied> GetServices();

        Task<ServicesSupplied> GetService(int serviceSuppliedId);

        Task<ServicesSupplied> GetDealership(int id);

        Task<IEnumerable<ServicesSupplied>> GetDealershipsByServicesasync(int serviceId);


        Task<ServicesSupplied> GetDealershipServicesPerDayAsync(int servicesSuppliedId, int dealershipId);

       
    }
}
