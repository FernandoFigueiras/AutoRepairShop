using AutoRepairShop.Web.Data.Entities;

namespace AutoRepairShop.Web.Data.Repositories
{
    public class CountyRepository : GenericRepository<County>, ICountyRepository
    {

        public CountyRepository(DataContext context) : base(context)
        {

        }
    }
}
