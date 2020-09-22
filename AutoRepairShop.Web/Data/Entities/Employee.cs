using Syncfusion.EJ2.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Data.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
       
        
        
        public DateTime? CreationDate { get; set; }
        
        

        
        public DateTime? UpdateDate { get; set; }
        
        

        
        public DateTime? DeactivationDate { get; set; }
        
        

        
        public bool IsActive { get; set; }




        public Dealership Dealership { get; set; }




        public User User { get; set; }




        public Department Department { get; set; }




        public EmployeePosition Position { get; set; }




        public string Role
        {
            get
            {
                return $"Employee/{this.Position.PositionName}/{this.Department.DepartmentName}";
            }
        }
    }
}
