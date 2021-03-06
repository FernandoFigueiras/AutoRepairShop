﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class Color : IEntity
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





        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }




        [Required]
        [Display(Name = "Color")]
        [MaxLength(20, ErrorMessage = "The name of the {0}, can only contain {1} characters")]
        public string ColorName { get; set; }
    }
}
