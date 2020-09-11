using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Helpers.Interfaces;
using AutoRepairShop.Web.Models.MainWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Helpers.Classes
{
    public class MainWindowConverterHelper : IMainWindowConverterHelper
    {

        public MainWindowViewModel ToMainWindowViewModel(User user)
        {
            return new MainWindowViewModel
            {
                UserImageUrl = user.ImageUrl,
            };
        }
    }

}

