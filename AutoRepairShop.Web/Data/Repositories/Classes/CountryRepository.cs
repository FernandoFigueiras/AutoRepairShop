using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context) : base(context)
        {
            _context = context;
        }




        public IQueryable GetCountriesWithDistricts()
        {
            return _context.Countries
                .Include(c => c.Districts)
                .OrderBy(c => c.CountryName);
        }



        public async Task<Country> GetCountryWithDistrictsAsync(int id)
        {
            return await _context.Countries
                .Include(c => c.Districts)
                .Where(c => c.Id == id)
                .OrderBy(c => c.CountryName)
                .FirstOrDefaultAsync();
        }
    }
}
