using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Helpers.Interfaces;

namespace AutoRepairShop.Web.Helpers.Classes
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

            if (user.FirstName != null && user.LastName != null && user.Address != null && user.ZipCode != null && user.ZipCode.ZipCode4 != null && user.ZipCode.ZipCode3 != null && user.PhoneNumber != null)
            {
                return true;
            }
            return false;

        }

    }
}
