using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class DealershipRepository : GenericRepository<Dealership>, IDealershipRepository
    {

        public DealershipRepository(DataContext context) : base(context)
        {

        }
    }
}
