using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Models.RepairViewModels
{
    public class DealershipRepairsViewModel
    {

        public int MyProperty { get; set; }



        [Display(Name ="Licence Plate")]
        public string LicencePlate { get; set; }




    }
}
