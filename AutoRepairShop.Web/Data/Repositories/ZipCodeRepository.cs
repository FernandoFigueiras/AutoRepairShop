using AutoRepairShop.Web.Data.Entities;

namespace AutoRepairShop.Web.Data.Repositories
{
    public class ZipCodeRepository : GenericRepository<ZipCode>, IZipCodeRepository
    {

        public ZipCodeRepository(DataContext context) : base(context)
        {
        }
    }
}
