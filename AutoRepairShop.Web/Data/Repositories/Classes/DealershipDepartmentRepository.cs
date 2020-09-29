using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class DealershipDepartmentRepository : GenericRepository<DealershipDepartment>, IDealershipDepartmentRepository
    {
        private readonly DataContext _context;

        public DealershipDepartmentRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public async Task AddDepartmentToDealershipAsync(Department department, Dealership dealership)
        {
            AddDepartment(department, dealership);

            await _context.SaveChangesAsync();
        }

        private void AddDepartment(Department department, Dealership dealership)
        {
            var DealershipDepartment = new DealershipDepartment
            {

                Dealership = dealership,
                Department = department,
            };

            _context.DealershipDepartments.Add(DealershipDepartment);
        }


        public  IEnumerable<DealershipDepartment> GetDealershipDepartments(int id)
        {
            return  _context.DealershipDepartments.Include(d => d.Department).Where(d => d.Dealership.Id == id);
        }


        public async Task DeleteDEalershipDepartmentsAsync(IEnumerable<DealershipDepartment> dealershipDepartments)
        {
            foreach (var item in dealershipDepartments)
            {
                _context.DealershipDepartments.RemoveRange(item);
            }

            await _context.SaveChangesAsync();
        }

    }
}
