using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Helpers.Interfaces;
using AutoRepairShop.Web.Models.Account;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Helpers.Classes
{
    public class UserHelper : IUserHelper
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserHelper(SignInManager<User> signInManager,
            UserManager<User> userManager)
        {

            _signInManager = signInManager;
            _userManager = userManager;
        }




        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
                false);
        }





        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }





        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }





        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }







        public async Task<User> GetUserByIdAsync(string userName)
        {
            return await _userManager.FindByIdAsync(userName);
        }






        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }






        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }





        public async Task<bool> IsUserEmailConfirmedAsync(User user)
        {
            return await _userManager.IsEmailConfirmedAsync(user);
        }





        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }





        public async Task<bool> UserExistsAsync(User user)
        {
            var Checkuser = await _userManager.FindByIdAsync(user.Id);
            if (Checkuser.Equals(user))
            {
                return true;
            }

            return false;
        }


        public async Task<string> GenerateResetPasswordTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }




        public async Task<IdentityResult> ResetPasswordAsync (User user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }



        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }



        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string nwePassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, nwePassword);
        }
    }
}
