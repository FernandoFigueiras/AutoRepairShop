using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class ServicesSuppliedRepository : GenericRepository<ServicesSupplied>, IServicesSuppliedRepository
    {
        private readonly DataContext _context;

        public ServicesSuppliedRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<ServicesSupplied> GetWithServicesByDealershipId(int id)
        {
            return _context.ServicesSupplied.Include(s => s.Dealership).Include(s => s.Service).Where(d => d.Dealership.Id == id).ToList();
        }


        public IEnumerable<ServicesSupplied> GetServices()
        {
            return _context.ServicesSupplied.Include(s => s.Service).ToList();
        } 


        public async Task<ServicesSupplied> GetService(int serviceSuppliedId)
        {
            return await _context.ServicesSupplied.Include(s => s.Service).Where(s => s.Id == serviceSuppliedId).FirstOrDefaultAsync();
        }


        public async Task<ServicesSupplied> GetDealership(int id)
        {
            return await _context.ServicesSupplied.FirstOrDefaultAsync(s => s.Dealership.Id==id);
        }



        public async Task<IEnumerable<ServicesSupplied>> GetDealershipsByServicesasync(int serviceId)
        {

            return await _context.ServicesSupplied
                .Include(s => s.Dealership)
                .Where(s => s.Service.Id == serviceId).ToListAsync();
        }


        public async Task<ServicesSupplied> GetDealershipServicesPerDayAsync(int servicesSuppliedId, int dealershipId)
        {
            return await _context.ServicesSupplied
                .FirstOrDefaultAsync(s => s.Id == servicesSuppliedId && s.Dealership.Id == dealershipId);
        }
    }
}
