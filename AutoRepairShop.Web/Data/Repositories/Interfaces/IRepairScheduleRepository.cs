using AutoRepairShop.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IRepairScheduleRepository : IGenericRepository<RepairSchedule>
    {

        IQueryable<RepairSchedule> GetDealershipRepairs(int id);


        IQueryable<RepairSchedule> GetUserRepairs(string id);


        IQueryable<RepairSchedule> GetRepairSchedule(int id);


        Task<RepairSchedule> GetRepairInfoByIdAsync(int id);

        Task<RepairSchedule> GetRepairScheduleFinishAsync(int id);
    }
}
