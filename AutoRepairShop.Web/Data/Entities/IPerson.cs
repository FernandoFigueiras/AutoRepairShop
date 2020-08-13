using System;

namespace AutoRepairShop.Web.Data.Entities
{
    public interface IPerson
    {

        bool? IsActive { get; set; }



        DateTime? CreationDate { get; set; }



        DateTime? UpdateDate { get; set; }



        string FirstName { get; set; }



        string LastName { get; set; }



        string Address { get; set; }



        string ZipCode { get; set; }



        string City { get; set; }


    }
}
