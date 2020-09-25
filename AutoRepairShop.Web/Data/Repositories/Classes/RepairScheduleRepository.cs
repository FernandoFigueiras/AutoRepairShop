using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class RepairScheduleRepository : GenericRepository<RepairSchedule>, IRepairScheduleRepository
    {
        private readonly DataContext _context;

        public RepairScheduleRepository(DataContext context) : base(context)
        {
            _context = context;
        }



        public IQueryable<RepairSchedule> GetDealershipRepairs(int id)
        {
            var test =  _context.RepairSchedules
                .Include(r => r.Repair)
                .ThenInclude(r => r.Department)
                .Include(r => r.Schedule)
                .ThenInclude(r => r.ActiveSchedule)
                .Include(r => r.Schedule)
                .ThenInclude(r => r.Vehicle)
                .Include(r => r.Schedule)
                .ThenInclude(r => r.ActiveSchedule)
                .ThenInclude(r => r.Services)
                .Where(r => r.Schedule.Dealership.Id == id);


            return test;
        }


        public IQueryable<RepairSchedule> GetUserRepairs(string id)
        {
            var test = _context.RepairSchedules
                .Include(r => r.Repair)
                .ThenInclude(r => r.Department)
                .Include(r => r.Schedule)
                .ThenInclude(r => r.ActiveSchedule)
                .Include(r => r.Schedule)
                .ThenInclude(r => r.Vehicle)
                .Include(r => r.Schedule)
                .ThenInclude(r => r.ActiveSchedule)
                .ThenInclude(r => r.Services)
                .Where(r => r.Schedule.Vehicle.User.Id == id);


            return test;
        }


        public IQueryable<RepairSchedule> GetRepairSchedule(int id)
        {
            var test = _context.RepairSchedules
               .Include(r => r.Repair)
               .ThenInclude(r => r.Department)
               .Include(r => r.Schedule)
               .ThenInclude(r => r.ActiveSchedule)
               .Include(r => r.Schedule)
               .ThenInclude(r => r.Vehicle)
               .Include(r => r.Schedule)
               .ThenInclude(r => r.ActiveSchedule)
               .ThenInclude(r => r.Services)
               .Where(r => r.Id == id);


            return test;
        }


        public async Task<RepairSchedule> GetRepairInfoByIdAsync(int id)
        {
            return await _context.RepairSchedules
                .Include(r => r.Repair)
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();
        }


        public async Task<RepairSchedule> GetRepairScheduleFinishAsync(int id)
        {
            var test = await _context.RepairSchedules
               .Include(r => r.Repair)
               .ThenInclude(r => r.Department)
               .Include(r => r.Schedule)
               .ThenInclude(r => r.ActiveSchedule)
               .Include(r => r.Schedule)
               .ThenInclude(r => r.Vehicle)
               .Include(r => r.Schedule)
               .ThenInclude(r => r.ActiveSchedule)
               .ThenInclude(r => r.Services)
               .Where(r => r.Id == id).FirstOrDefaultAsync();


            return test;
        }

    }
}
