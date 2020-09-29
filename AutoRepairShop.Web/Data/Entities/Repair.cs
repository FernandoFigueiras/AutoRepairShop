using System;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class Repair : IEntity
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



        [Display(Name = "Is active")]
        public bool IsActive { get; set; }




        public Department Department { get; set; }




        [Display(Name ="Service Done")]
        public string ServiceDone { get; set; }




        [Display(Name ="Work Hours")]
        public double WorkHours { get; set; }


    }
}
