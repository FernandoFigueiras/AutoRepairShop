using System;

namespace AutoRepairShop.Web.Data.Entities
{
    public class DealershipDepartment: IEntity
    {

      

        public int Id { get; set; }




        public DateTime? CreationDate { get; set; }
       
        
        
        
        public DateTime? UpdateDate { get; set; }
       
        
        
        
        public DateTime? DeactivationDate { get; set; }
       
        
        
        
        public bool IsActive { get; set; }




        public Dealership Dealership { get; set; }




        public Department Department { get; set; }
    }
}
