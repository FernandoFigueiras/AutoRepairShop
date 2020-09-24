using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class RepairHistoryRepository : GenericRepository<RepairHistory>, IRepairHistoryRepository
    {

        public RepairHistoryRepository(DataContext context) : base(context)
        {

        }
    }
}
