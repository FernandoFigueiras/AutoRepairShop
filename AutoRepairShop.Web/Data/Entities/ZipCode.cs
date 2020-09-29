using System;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class ZipCode : IEntity
    {

        public int Id { get; set; }



        [Display(Name = "Creation Date")]
        public DateTime? CreationDate { get; set; }




        [Display(Name = "Update Date")]
        public DateTime? UpdateDate { get; set; }




        [Display(Name = "Deactivation Date")]
        public DateTime? DeactivationDate { get; set; }



        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }




        [Display(Name = "Zip Code")]
        [Required]
        public string ZipCode4 { get; set; }





        [Display(Name = "Zip Code Street number")]
        [Required]
        public string ZipCode3 { get; set; }



        public int CityId { get; set; }



        public City City { get; set; }



        [Display(Name ="Zip Code")]
        public string FullZipCode
        {
            get
            {
                return $"{ZipCode4} - {ZipCode3}";
            }
        }


    }
}
