using System;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models.ActiveScheduleViewModel
{
    public class CompleteScheduleByDealershipNoUserViewModel
    {
        public string UserId { get; set; }


        public int VehicleId { get; set; }



        public int ServiceId { get; set; }



        public int DealershipId { get; set; }



        public string DaysToDisable { get; set; }


        [Required]
        public DateTime Day { get; set; }



        [MaxLength(500)]
        public string Remarks { get; set; }


        [Required]
        [MaxLength(10)]
        public string Mileage { get; set; }
    }
}
