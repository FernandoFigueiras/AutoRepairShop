using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Models.Account;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Helpers.Interfaces
{
    public interface IUserHelper
    {

        Task<SignInResult> LoginAsync(LoginViewModel model);




        Task LogOutAsync();




        Task<User> GetUserByEmailAsync(string email);




        Task<IdentityResult> AddUserAsync(User user, string password);




        Task AddUserToRoleAsync(User user, string role);




        Task CheckRoleAsync(string roleName);




        Task<bool> IsUSerInRoleAsync(User user, string roleName);



        Task RemoveFromRoleAsync(User user, string role);




        Task<User> GetUserByIdAsync(string userName);




        Task<string> GenerateEmailConfirmationTokenAsync(User user);




        Task<IdentityResult> ConfirmEmailAsync(User user, string token);



        Task<bool> IsUserEmailConfirmedAsync(User user);



        Task<IdentityResult> UpdateUserAsync(User user);




        Task<string> GenerateResetPasswordTokenAsync(User user);




        Task<IdentityResult> ResetPasswordAsync (User user, string token, string password);




        Task<bool> CheckPasswordAsync(User user, string password);




        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string nwePassword);



        Task<IdentityResult> DeleteUserAsync(User user);




    }
}
