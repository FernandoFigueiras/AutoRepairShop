using System;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class EmployeePosition : IEntity
    {
        public int Id { get; set; }




        public DateTime? CreationDate { get; set; }




        public DateTime? UpdateDate { get; set; }




        public DateTime? DeactivationDate { get; set; }




        public bool IsActive { get; set; }




        [Display(Name = "Position Name")]
        public string PositionName { get; set; }

    }
}
