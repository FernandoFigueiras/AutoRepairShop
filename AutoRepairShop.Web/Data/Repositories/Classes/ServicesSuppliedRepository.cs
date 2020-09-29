using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class ServicesSuppliedRepository : GenericRepository<DealershipService>, IServicesSuppliedRepository
    {
        private readonly DataContext _context;

        public ServicesSuppliedRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<DealershipService> GetWithServicesByDealershipId(int id)
        {
            return _context.DealershipServices.Include(s => s.Dealership).Include(s => s.Service).Where(d => d.Dealership.Id == id && d.Service.IsActive==true).ToList();
        }


        public IEnumerable<DealershipService> GetServices()
        {
            return _context.DealershipServices.Include(s => s.Service).ToList();
        } 


        public async Task<DealershipService> GetService(int serviceSuppliedId)
        {
            return await _context.DealershipServices.Include(s => s.Service).Where(s => s.Service.Id == serviceSuppliedId).FirstOrDefaultAsync();
        }


        public async Task<DealershipService> GetDealership(int id)
        {
            return await _context.DealershipServices.FirstOrDefaultAsync(s => s.Dealership.Id==id);
        }



        public async Task<IEnumerable<DealershipService>> GetDealershipsByServicesasync(int serviceId)
        {

            return await _context.DealershipServices
                .Include(s => s.Dealership)
                .Where(s => s.Service.Id == serviceId).ToListAsync();
        }


        public async Task<DealershipService> GetDealershipServicesPerDayAsync(int servicesSuppliedId, int dealershipId)
        {
            return await _context.DealershipServices
                .FirstOrDefaultAsync(s => s.Service.Id == servicesSuppliedId && s.Dealership.Id == dealershipId);
        }



        public async Task AddServicesToDealershipAsync(Service service, Dealership dealership)
        {
            AddService(service, dealership);

            await _context.SaveChangesAsync();
        }

        private void AddService(Service serviceId, Dealership dealershipId)
        {
            var servicesSupplied = new DealershipService
            {
                
                Dealership = dealershipId,
                Service = serviceId,
            };

            _context.DealershipServices.Add(servicesSupplied);
        }


        public IEnumerable<DealershipService> GetServicesSupplied (int dealershipId)
        {
            return _context.DealershipServices.Include(s =>s.Service).Where(s => s.Dealership.Id == dealershipId);
        }
        
    }
}
