using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class User : IdentityUser, IPerson
    {

        [Display(Name = "Is Active")]
        public bool? IsActive { get; set; }

    
        public bool CanLogin { get; set; }


        [Display(Name = "Creation Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? CreationDate { get; set; }



        [Display(Name = "Update Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? UpdateDate { get; set; }



        [Display(Name = "First Name")]
        public string FirstName { get; set; }



        [Display(Name = "Last Name")]
        public string LastName { get; set; }


   
        public string Address { get; set; }


        [Display(Name = "Zip Code")]
        public int ZipCodeId { get; set; }


     
        public ZipCode ZipCode { get; set; }


        [Display(Name = "Taxpayer Number")]
        public string TaxPayerNumber { get; set; }



        public string City { get; set; }



        [Display(Name ="Image")]
        public string ImageUrl { get; set; }



        public string FullName
        {
            get
            {
                return $"{this.FirstName} {this.LastName}";
            }
        }

    }
}
