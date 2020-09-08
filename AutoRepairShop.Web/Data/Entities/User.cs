using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Data.Entities
{
    public class User : IdentityUser, IPerson
    {


        public bool? IsActive { get; set; }
        
        

        
        public DateTime? CreationDate { get; set; }
        
        

        
        public DateTime? UpdateDate { get; set; }


        [Required]
        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters long")]
        public string FirstName { get; set; }



        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters long")]
        public string LastName { get; set; }



        [Required]
        [MaxLength(150, ErrorMessage = "The field {0} only can contain {1} characters long")]
        public string Address { get; set; }



        public int ZipCodeId { get; set; }


     
        public ZipCode ZipCode { get; set; }



        public string TaxPayerNumber { get; set; }



        public string City { get; set; }



        [Display(Name ="Image")]
        public string ImageUrl { get; set; }



    }
}
