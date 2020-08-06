using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class Vehicle : IEntity
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
        [Display(Name = "Licence PLate")]
        [MaxLength(10, ErrorMessage = "The field {0} only can contain {1} characters long")]
        public string LicencePLate { get; set; }





        [Required]
        public Brand Brands { get; set; }




        [Display(Name = "Other model not listed")]
        public string ModelName { get; set; }



        [Required]
        [Display(Name = "Engine Capacity")]
        public string EngineCapacity { get; set; }




        [Required]
        public Fuel Fuel { get; set; }




        
        [MaxLength(10, ErrorMessage = "The field {0} only can contain {1} characters long")]
        public string Color { get; set; }



    }
}
