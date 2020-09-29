using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class DealershipServiceRepository : GenericRepository<DealershipService>, IDealershipServiceRepository
    {
        private readonly DataContext _context;

        public DealershipServiceRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public async Task DeleteServicesFromDealershipAsync(IEnumerable<DealershipService> services)
        {

            foreach (var item in services)
            {
                _context.DealershipServices.RemoveRange(item);
            }

            await _context.SaveChangesAsync();
        }
    }
}
