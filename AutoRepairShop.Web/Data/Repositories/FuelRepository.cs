using AutoRepairShop.Web.Data.Entities;

namespace AutoRepairShop.Web.Data.Repositories
{
    public class FuelRepository : GenericRepository<Fuel>, IFuelRepository
    {

        public FuelRepository(DataContext context) : base(context)
        {

        }
    }
}
