using AutoRepairShop.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IActiveScheduleRepository : IGenericRepository<ActiveSchedule>
    {


        Task<IEnumerable<DealershipService>> GetDealershipsWithServicesAsync(int serviceId);

        Task<List<ActiveSchedule>> GetDaysByServiceId(int serviceId);
    }
}
