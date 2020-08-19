using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private readonly DataContext _context;

        public CityRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public async Task<bool> ExistsCityInDistrictAsync(int districtId, City city)
        {
            return await _context.Cities.AnyAsync(c => c.DistrictId == districtId && c.CityName == city.CityName && c.Id != city.Id);

        }
    }
}
