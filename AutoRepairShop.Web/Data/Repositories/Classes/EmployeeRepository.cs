using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataContext context) : base(context)
        {

        }
    }
}
