﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class DealershipService: IEntity
    {

        public int Id { get; set; }



        [Display(Name = "Creation Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? CreationDate { get; set; }




        [Display(Name = "Update Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? UpdateDate { get; set; }




        [Display(Name = "Deactivation Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? DeactivationDate { get; set; }




        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }


        

        public Dealership Dealership { get; set; }



        public Service Service { get; set; }



        [Display(Name ="Services Per Day")]
        public int ServicesPerDay { get; set; }



    }
}
