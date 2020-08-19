using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class City : IEntity
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


        [Display(Name = "City")]
        [Required]
        public string CityName { get; set; }



        public int DistrictId { get; set; }



        public District District { get; set; }



        public ICollection<ZipCode> ZipCodes { get; set; }


    }
}
