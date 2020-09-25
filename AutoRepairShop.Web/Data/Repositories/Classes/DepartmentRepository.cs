using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context) : base(context)
        {
            _context = context;
        }



        public async Task<List<Department>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }
    }
}
