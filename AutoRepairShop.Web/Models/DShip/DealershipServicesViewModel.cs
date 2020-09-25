using AutoRepairShop.Web.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.DShip
{
    public class DealershipServicesViewModel
    {


        public int DealershipId { get; set; }


        public string DealershipName { get; set; }



        public List<DealershipService> Services { get; set; }


        public bool IsActive { get; set; }



        [Display(Name = "Services per day")]
        public int ServicesPerDay { get; set; }


    }
}
