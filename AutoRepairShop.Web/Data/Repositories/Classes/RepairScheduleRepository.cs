using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Repositories.Classes
{
    public class RepairScheduleRepository: GenericRepository<RepairSchedule>, IRepairScheduleRepository
    {
        public RepairScheduleRepository(DataContext context) : base(context)
        {

        }
    }
}
