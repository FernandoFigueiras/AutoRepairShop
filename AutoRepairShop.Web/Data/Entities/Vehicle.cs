using System;
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





        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }





        [Required]
        [Display(Name = "Licence PLate")]
        [MaxLength(10, ErrorMessage = "The field {0} only can contain {1} characters long")]
        public string LicencePlate { get; set; }







        [Required]
        [Display(Name = "Brand")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Brand")]
        public int BrandId { get; set; }








        [Display(Name = "Model")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Model")]
        public int ModelId { get; set; }






        [Required]
        [Display(Name = "Engine Capacity")]
        public string EngineCapacity { get; set; }




        public double Mileage { get; set; }




        [Display(Name ="Vehicle Identification NUmber")]
        public string VIN { get; set; }





        [Display(Name = "Fuel")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Fuel")]
        public int FuelId { get; set; }





        [Display(Name = "Fuel")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Color")]
        public int ColorId { get; set; }





        public Color Color { get; set; }





        public BrandModel Model { get; set; }





        public Fuel Fuel { get; set; }




    
        public User User { get; set; }

    }
}
