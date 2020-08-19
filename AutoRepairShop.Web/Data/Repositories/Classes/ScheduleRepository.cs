using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {

        public ScheduleRepository(DataContext context) : base(context)
        {
        }
    }
}
