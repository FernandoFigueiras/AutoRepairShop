﻿using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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


        public IQueryable<Employee> GetEmployeeFullInfoAsync()
        {
            return _context.Employees
                .Include(e => e.Dealership)
                .Include(e => e.Department)
                .Include(e => e.Position)
                .Include(e => e.User);
                
        }
    }
}
