﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class Country : IEntity
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



        [Required]
        [Display(Name = "Country")]
        public string CountryName { get; set; }




        public ICollection<District> Districts { get; set; }



        [Display(Name ="Districts Count")]
        public int DistrictsCount
        {
            get
            {
                return this.Districts == null ? 0 : Districts.Count;
            }
        }

    }
}
