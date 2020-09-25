using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Models.ActiveScheduleViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class ActiveScheduleRepository : GenericRepository<ActiveSchedule>, IActiveScheduleRepository
    {
        private readonly DataContext _context;

        public ActiveScheduleRepository(DataContext context) : base(context)
        {
            _context = context;
        }



       
        public async Task<IEnumerable<DealershipService>> GetDealershipsWithServicesAsync(int serviceId)
        {
            var services = await _context.DealershipServices
                .Include(s => s.Service)
                .Include(s => s.Dealership)
                .Where(s => s.Service.Id == serviceId).ToListAsync();

            return services;
        }


        public async Task<List<ActiveSchedule>> GetDaysByServiceId(int serviceId)
        {
            var list = await _context.ActiveSchedules.Include(s => s.Services).Where(s => s.Services.Id == serviceId).OrderBy(s => s.ScheduleDay).ToListAsync();

            return list;
        }

    }
}
