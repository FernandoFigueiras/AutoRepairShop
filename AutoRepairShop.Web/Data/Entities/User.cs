using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class User : IdentityUser, IPerson
    {


        public bool? IsActive { get; set; }


        public bool CanLogin { get; set; }


        public DateTime? CreationDate { get; set; }
        
        

        
        public DateTime? UpdateDate { get; set; }



        public string FirstName { get; set; }



   
        public string LastName { get; set; }


   
        public string Address { get; set; }



        public int ZipCodeId { get; set; }


     
        public ZipCode ZipCode { get; set; }



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
