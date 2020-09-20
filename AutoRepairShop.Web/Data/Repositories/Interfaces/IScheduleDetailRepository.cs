using AutoRepairShop.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IScheduleDetailRepository : IGenericRepository<ScheduleDetail>
    {

        IQueryable<ScheduleDetail> GetSchedulesDetail(string userId);




        Task<ScheduleDetail> GetScheduleDetailAsync(int activeScheduleId);



        Task<bool> IsDuplicatedSchedulesForSameServiceAsync(int vehicleId, int serviceId);



        Task<ScheduleDetail> GetScheduleDetailByIdAsync(int Id);
    }
}
