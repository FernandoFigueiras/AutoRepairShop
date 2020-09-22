using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public IQueryable<Department> GetDealershipDepartments(int dealershipId)
        {
            return  _context.Departments.Include(d => d.Dealership).Where(d => d.Dealership.Id == dealershipId);
        }
    }
}
