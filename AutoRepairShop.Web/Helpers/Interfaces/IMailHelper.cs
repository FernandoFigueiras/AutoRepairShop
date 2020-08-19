using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Helpers.Interfaces
{
    public interface IMailHelper
    {

        void SendEmail(string to, string subject, string body);
    }
}
