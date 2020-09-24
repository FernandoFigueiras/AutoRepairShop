using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public IQueryable<Employee> GetEmployeesFullInfoAsync()
        {
            return _context.Employees
                .Include(e => e.Dealership)
                .Include(e => e.Department)
                .Include(e => e.User);

        }


        public async Task<Employee> GetEmployeeFullInfoAsync(int Id)
        {
            return await _context.Employees
                .Include(e => e.Dealership)
                .Include(e => e.Department)
                .Include(e => e.User)
                .Where(e => e.Id == Id)
                .FirstOrDefaultAsync();
        }



        public async Task<Employee> GetFullEmployeeByUserAsync(string userId)
        {
            return await _context.Employees
                .Include(e => e.Dealership)
                .Include(e => e.Department)
                .Include(e => e.User)
                .Where(e => e.User.Id == userId)
                .FirstOrDefaultAsync();
        }
    }
}
