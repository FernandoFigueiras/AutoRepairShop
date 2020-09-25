using System;

namespace AutoRepairShop.Web.Data.Entities
{
    public class RepairHistory : IEntity
    {
        public int Id { get; set; }


        public DateTime? CreationDate { get; set; }



        public DateTime? UpdateDate { get; set; }



        public DateTime? DeactivationDate { get; set; }



        public bool IsActive { get; set; }




        public string LicencePlate { get; set; }




        public string Mileage { get; set; }



        public int DealershipId { get; set; }



        public string Dealership { get; set; }



        public int ServiceId { get; set; }



        public string Service { get; set; }



        public string Remarks { get; set; }




        public DateTime RepairDate { get; set; }




        public string RepairHours { get; set; }



        public string RepairRemarks { get; set; }



    }
}
