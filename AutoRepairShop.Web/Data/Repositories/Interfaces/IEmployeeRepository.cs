using AutoRepairShop.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {

        IQueryable<Employee> GetEmployeesFullInfoAsync();


        Task<Employee> GetEmployeeFullInfoAsync(int Id);
    }
}
