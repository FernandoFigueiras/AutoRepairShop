using AutoRepairShop.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Interfaces
{
    public interface IDealershipDepartmentRepository : IGenericRepository<DealershipDepartment>
    {

        Task AddDepartmentToDealershipAsync(Department department, Dealership dealership);


        IEnumerable<DealershipDepartment> GetDealershipDepartments(int id);


        Task DeleteDEalershipDepartmentsAsync(IEnumerable<DealershipDepartment> dealershipDepartments);
    }
}
