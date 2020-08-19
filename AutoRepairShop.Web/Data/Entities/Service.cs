using System;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class Service : IEntity
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




        [Display(Name = "ServiceType")]
        [Required]
        public string ServiceType { get; set; }



        [Display(Name = "Description")]
        public string ServiceDescription { get; set; }


    }
}
