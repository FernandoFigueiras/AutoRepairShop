using AutoRepairShop.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IServicesSuppliedRepository : IGenericRepository<DealershipService>
    {

        IEnumerable<DealershipService> GetWithServicesByDealershipId(int id);

        IEnumerable<DealershipService> GetServices();

        Task<DealershipService> GetService(int serviceSuppliedId);

        Task<DealershipService> GetDealership(int id);

        Task<IEnumerable<DealershipService>> GetDealershipsByServicesasync(int serviceId);


        Task<DealershipService> GetDealershipServicesPerDayAsync(int servicesSuppliedId, int dealershipId);

        Task AddServicesToDealershipAsync(Service service, Dealership dealership);


        IEnumerable<DealershipService> GetServicesSupplied(int dealershipId);
    }
}
