using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class RepairRepository : GenericRepository<Repair>, IRepairRepository
    {
        private readonly DataContext _context;

        public RepairRepository(DataContext context) : base(context)
        {
            _context = context;
        }


    }
}
