using System;

namespace AutoRepairShop.Web.Models.ActiveScheduleViewModel
{
    public class ScheduleDetailsViewModel
    {
        public int ActiveScheduleId { get; set; }

        public string LicencePLate { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Mileage { get; set; }

        public string Dealership { get; set; }

        public string Service { get; set; }

        public DateTime Date { get; set; }

        public string Remarks { get; set; }

    }
}
