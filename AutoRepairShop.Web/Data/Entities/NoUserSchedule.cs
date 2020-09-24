using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class NoUserSchedule
    {

        public string UserId { get; set; }




        [Display(Name = "Licence PLate")]
        [MaxLength(10, ErrorMessage = "The field {0} only can contain {1} characters long")]
        public string LicencePlate { get; set; }





        [Display(Name = "Licence PLate")]
        [MaxLength(10, ErrorMessage = "The field {0} only can contain {1} characters long")]
        public string Brand { get; set; }





        [Display(Name = "Licence PLate")]
        [MaxLength(10, ErrorMessage = "The field {0} only can contain {1} characters long")]
        public string Model { get; set; }





        [Display(Name = "Engine Capacity")]
        public string EngineCapacity { get; set; }




        public double Mileage { get; set; }



        public int Fuel { get; set; }




        public int Color { get; set; }




        public string FirstName { get; set; }




        public string LastName { get; set; }




        public string PhoneNumber { get; set; }



        public string TaxPayerNumber { get; set; }

    }
}
