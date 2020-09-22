using AutoRepairShop.Web.Data.Entities;
using System.Linq;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {

        IQueryable<Department> GetDealershipDepartments(int dealershipId);
    }
}
