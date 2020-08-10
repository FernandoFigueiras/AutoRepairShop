using AutoRepairShop.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories
{
    public class FuelRepository : GenericRepository<Fuel>, IFuelRepository
    {
        private readonly DataContext _context;

        public FuelRepository(DataContext context) : base(context)
        {
            _context = context;
        }



        public async Task<string> GetFuelNameByIdAsync(int id)
        {
            var fuel = await _context.Fuels.FirstOrDefaultAsync(f => f.Id == id);
            return fuel.FuelType;
        }
    }
}
