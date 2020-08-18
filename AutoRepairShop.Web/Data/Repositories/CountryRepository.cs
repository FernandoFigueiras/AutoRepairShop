using AutoRepairShop.Web.Data.Entities;

namespace AutoRepairShop.Web.Data.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {

        public CountryRepository(DataContext context) : base(context)
        {

        }
    }
}
