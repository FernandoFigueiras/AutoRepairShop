using System;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class RepairHistory : IEntity
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



        [Display(Name = "Licence Plate")]
        public string LicencePlate { get; set; }




        public string Mileage { get; set; }


        [Display(Name = "Dealership Id")]
        public int DealershipId { get; set; }



        public string Dealership { get; set; }


        [Display(Name = "Service Id")]
        public int ServiceId { get; set; }



        public string Service { get; set; }



        public string Remarks { get; set; }



        [Display(Name = "Repair Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime RepairDate { get; set; }



        [Display(Name = "Repair Hours")]
        public string RepairHours { get; set; }



        [Display(Name = "Repair Remarks")]
        public string RepairRemarks { get; set; }



    }
}
