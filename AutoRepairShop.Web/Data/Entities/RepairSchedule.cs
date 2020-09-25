using System;

namespace AutoRepairShop.Web.Data.Entities
{
    public class RepairSchedule : IEntity
    {
        public int Id { get; set; }




        public DateTime? CreationDate { get; set; }




        public DateTime? UpdateDate { get; set; }




        public DateTime? DeactivationDate { get; set; }




        public bool IsActive { get; set; }




        public ScheduleDetail Schedule { get; set; }




        public Repair Repair { get; set; }

    }
}
