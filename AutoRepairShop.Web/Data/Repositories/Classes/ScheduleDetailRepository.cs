using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class ScheduleDetailRepository : GenericRepository<ScheduleDetail>, IScheduleDetailRepository
    {
        private readonly DataContext _context;

        public ScheduleDetailRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<ScheduleDetail> GetSchedulesDetail(string userId)
        {
            var detail = _context.ScheduleDetails
                .Include(d => d.ActiveSchedule)
                .ThenInclude(a => a.Services)
                .Include(d => d.Dealership)
                .Include(d => d.Vehicle)
                .Where(d => d.Vehicle.User.Id == userId && d.IsActive==true);


            return detail;
        }


        public async Task<ScheduleDetail> GetScheduleDetailAsync(int activeScheduleId)
        {
            var detail = await _context.ScheduleDetails
               .Include(d => d.ActiveSchedule)
               .ThenInclude(a => a.Services)
               .Include(d => d.Dealership)
               .Include(d => d.Vehicle)
               .Include(v => v.Vehicle.Model.Brand)
               .Include(v => v.Vehicle.Model)
               .Include(v => v.Vehicle.Fuel)
               .Where(d => d.ActiveSchedule.Id == activeScheduleId).FirstOrDefaultAsync();

            return detail;
        }


        public async Task<bool> IsDuplicatedSchedulesForSameServiceAsync(int vehicleId, int serviceId)
        {
            var list = await _context.ScheduleDetails
                .Include(s => s.ActiveSchedule)
                .ThenInclude(a => a.Services)
                .Where(s => s.Vehicle.Id == vehicleId)
                .ToListAsync();

            var count = 0;
            foreach (var item in list)
            {
                if (item.ActiveSchedule.Services.Id == serviceId)
                {
                    count++;
                }
            }

            if (count > 0)
            {
                return true;
            }

            return false;
        }
        

        public async Task<ScheduleDetail> GetScheduleDetailByIdAsync(int Id)
        {
            return await _context.ScheduleDetails
               .Include(d => d.ActiveSchedule)
               .ThenInclude(a => a.Services)
               .Include(d => d.Dealership)
               .Include(d => d.Vehicle)
               .Where(d => d.Id == Id)
               .FirstOrDefaultAsync();
        }



        public IQueryable<ScheduleDetail> GetScheduleForDealership(int id)
        {
            return _context.ScheduleDetails
                .Include(d => d.ActiveSchedule)
                .ThenInclude(a => a.Services)
                .Include(d => d.Dealership)
                .Include(d => d.Vehicle)
                .Where(d => d.Dealership.Id == id && d.IsActive==true);
        }


        public async Task<ScheduleDetail> GetScheduleDetailByActiveSchedule(int id)
        {
            return await _context.ScheduleDetails.FirstOrDefaultAsync(a => a.ActiveSchedule.Id == id);
        }

    }
}
