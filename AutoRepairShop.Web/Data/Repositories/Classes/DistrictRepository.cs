using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class DistrictRepository : GenericRepository<District>, IDistrictRepository
    {
        private readonly DataContext _context;

        public DistrictRepository(DataContext context) : base(context)
        {
            _context = context;
        }





        public async Task<bool> ExistsInCountryAsync(int id, string districtName, int districtId)
        {
            return await _context.Districts.AnyAsync(d => d.CountryId == id && d.DistrictName == districtName && d.Id != districtId);
            
        }
    }
}
