﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class Fuel : IEntity
    {

        public int Id { get; set; }



        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }




        [Display(Name = "Update Date")]
        public DateTime? UpdateDate { get; set; }




        [Display(Name = "Deactivation Date")]
        public DateTime? DeactivationDate { get; set; }




        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }




        [Required]
        [Display(Name = "Fuel")]
        public string FuelType { get; set; }


    }
}
