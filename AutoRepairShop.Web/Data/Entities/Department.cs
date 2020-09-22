using System;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class Department : IEntity
    {
        public int Id { get; set; }



        public DateTime? CreationDate { get; set; }



        public DateTime? UpdateDate { get; set; }



        public DateTime? DeactivationDate { get; set; }



        public bool IsActive { get; set; }


        [Required]
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }



        public Dealership Dealership { get; set; }

    }
}
