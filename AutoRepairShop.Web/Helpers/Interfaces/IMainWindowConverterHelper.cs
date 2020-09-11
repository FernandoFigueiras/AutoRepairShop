using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Models.MainWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Helpers.Interfaces
{
    public interface IMainWindowConverterHelper
    {

        MainWindowViewModel ToMainWindowViewModel(User user);


    }
}
