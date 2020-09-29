using System;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }


        [Display(Name ="Creation Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? CreationDate { get; set; }


        [Display(Name = "Update Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? UpdateDate { get; set; }


        [Display(Name = "Deactivation Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? DeactivationDate { get; set; }




        public bool IsActive { get; set; }




        public Dealership Dealership { get; set; }




        public User User { get; set; }




        public Department Department { get; set; }



        public string Role
        {
            get
            {
                return $"Employee/{this.Department.DepartmentName}";
            }
        }
    }
}
