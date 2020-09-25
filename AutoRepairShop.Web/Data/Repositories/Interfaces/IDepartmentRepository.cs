using AutoRepairShop.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {

        Task<List<Department>> GetDepartments();
    }
}
