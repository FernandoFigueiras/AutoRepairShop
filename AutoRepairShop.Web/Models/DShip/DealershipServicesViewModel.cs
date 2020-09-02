using AutoRepairShop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Models.DShip
{
    public class DealershipServicesViewModel
    {


        public int DealershipId { get; set; }


        public string DealershipName { get; set; }



        public List<ServicesSupplied> Services { get; set; }

    }
}
