using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Entities
{
    public class ScheduleDetail
    {

        public int Id { get; set; }


        public Vehicle Vehicle { get; set; }



        public Schedule Schedule { get; set; }


    }
}
