using AutoRepairShop.Web.Data.Entities;

namespace AutoRepairShop.Web.Data.Repositories
{
    public class DistrictRepository : GenericRepository<District>, IDistrictRepository
    {

        public DistrictRepository(DataContext context) : base(context)
        {

        }
    }
}
