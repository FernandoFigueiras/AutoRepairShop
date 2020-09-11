namespace AutoRepairShop.Web.Data.Entities
{
    public class ScheduleDetail
    {

        public int Id { get; set; }


        public Vehicle Vehicle { get; set; }



        public ActiveSchedule ActiveSchedule { get; set; }



        public Dealership Dealership { get; set; }


    }
}
