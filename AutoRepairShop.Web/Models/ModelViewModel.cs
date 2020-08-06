using System.ComponentModel.DataAnnotations;

namespace AutoRepairShop.Web.Models
{
    public class ModelViewModel
    {


        public int BrandId { get; set; }




        public int ModelId { get; set; }




        [Required]
        [Display(Name = "Model")]
        [MaxLength(50, ErrorMessage = "The field {0 }only can contain {1} characters")]
        public string Name { get; set; }



    }
}
