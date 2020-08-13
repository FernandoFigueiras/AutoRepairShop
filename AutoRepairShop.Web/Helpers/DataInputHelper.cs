﻿using AutoRepairShop.Web.Data.Entities;

namespace AutoRepairShop.Web.Helpers
{
    public class DataInputHelper : IDataInputHelper
    {
        private readonly IUserHelper _userHelper;

        public DataInputHelper(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        public bool IsUserDataInserted(User user)
        {

            if (user.FirstName != null && user.LastName != null && user.Address != null && user.ZipCode != null && user.City != null && user.PhoneNumber != null)
            {
                return true;
            }
            return false;

        }

    }
}
