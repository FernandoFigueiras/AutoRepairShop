using AutoRepairShop.Web.Data.Entities;

namespace AutoRepairShop.Web.Data.Repositories
{
    public class PlaceRepository : GenericRepository<ZipCode>, IPlaceRepository
    {

        public PlaceRepository(DataContext context) : base(context)
        {

        }
    }
}
