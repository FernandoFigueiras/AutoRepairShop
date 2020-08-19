using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class CountyRepository : GenericRepository<County>, ICountyRepository
    {

        public CountyRepository(DataContext context) : base(context)
        {

        }
    }
}
