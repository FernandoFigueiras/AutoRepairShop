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



        public IQueryable<ScheduleDetail> GetScheduleDetail (string userId)
        {
            var detail = _context.ScheduleDetails
                .Include(d => d.ActiveSchedule)
                .Include(d => d.Dealership)
                .Include(d => d.Vehicle)
                .Where(d => d.Vehicle.User.Id == userId);


            return detail;
        }



        public async Task<IEnumerable<ServicesSupplied>> GetDealershipsWithServicesAsync(int serviceId)
        {
            var services = await _context.ServicesSupplied
                .Include(s => s.Service)
                .Include(s => s.Dealership)
                .Where(s => s.Service.Id == serviceId).ToListAsync();

            return services;
        }

    }
}
