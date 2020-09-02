using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class DealershipRepository : GenericRepository<Dealership>, IDealershipRepository
    {
        private readonly DataContext _context;

        public DealershipRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public async Task<Dealership> GetWithZipCodeAsync(int dealershipId)
        {
            return await _context.Dealerships.Include(z => z.ZipCode).Where(d => d.Id == dealershipId).FirstOrDefaultAsync();
        } 


        public IQueryable GetAllWithZipCode()
        {
            return _context.Dealerships.Include(z => z.ZipCode);
        }


        public async Task<Dealership> GetByNameAsync( string dealershipName)
        {
            return await _context.Dealerships.FirstOrDefaultAsync(d => d.DealerShipName == dealershipName);
        }

    }
}
