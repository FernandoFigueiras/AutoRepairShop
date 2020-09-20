using System;

namespace AutoRepairShop.Web.Data.Entities
{
    public class ScheduleDetail: IEntity
    {

        public DateTime? CreationDate { get; set; }
        public DateTime? UpdateDate { get ; set; }
        public DateTime? DeactivationDate { get; set; }
        public bool IsActive { get; set; }


        public int Id { get; set; }



        public Vehicle Vehicle { get; set; }



        public ActiveSchedule ActiveSchedule { get; set; }



        public Dealership Dealership { get; set; }
        
    }
}
