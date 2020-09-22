using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context) : base(context)
        {
            _context = context;
        }


    }
}
