using AutoRepairShop.Web.Data.Entities;
using System.Linq;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {

        IQueryable<Employee> GetEmployeeFullInfoAsync();
    }
}
