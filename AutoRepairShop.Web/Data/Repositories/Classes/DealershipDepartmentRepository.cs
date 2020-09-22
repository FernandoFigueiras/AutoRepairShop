using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class DealershipDepartmentRepository : GenericRepository<DealershipDepartment>, IDealershipDepartmentRepository
    {

        public DealershipDepartmentRepository(DataContext context) : base(context)
        {

        }
    }
}
