using AutoRepairShop.Web.Data.Entities;

namespace AutoRepairShop.Web.Models.RepairViewModels
{
    public class FinishRepairViewModel
    {


        public int VehicleId { get; set; }


        public Vehicle Vehicle { get; set; }


        public int RepairId { get; set; }


        public Repair Repair { get; set; }


        public int ScheduleDetailId { get; set; }


        public ScheduleDetail ScheduleDetail { get; set; }


        public int RepairScheduleID { get; set; }


        public RepairSchedule RepairSchedule { get; set; }


        public int ActiveScheduleID { get; set; }


        public ActiveSchedule ActiveSchedule { get; set; }
    }
}
