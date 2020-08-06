using AutoRepairShop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Models
{
    public class AddVehicleViewModel : Vehicle
    {



        public int BrandId { get; set; }



    }
}
