using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        private readonly DataContext _context;

        public ServiceRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<Service>> GetAllServicesAsync()
        {
            return await _context.Services.Where(s => s.IsActive == true).ToListAsync();
        }

        public IEnumerable<Service> GetAllServices()
        {
            return  _context.Services.Where(s => s.IsActive == true);
        }


    }
}
