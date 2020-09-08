using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Helpers;
using AutoRepairShop.Web.Helpers.Interfaces;
using AutoRepairShop.Web.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading;
using System.Threading.Tasks;

namespace AutoRepairShop.Web.Controllers.BackAndFrontOffice
{
    public class AccountsController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IDataInputHelper _dataInputHelper;
        private readonly IZipCodeRepository _zipCodeRepository;
        private readonly IImageHelper _imageHelper;
        private readonly ICityRepository _cityRepository;

        public AccountsController(IUserHelper userHelper,
            IConverterHelper converterHelper,
            IMailHelper mailHelper,
            IDataInputHelper dataInputHelper,
            IZipCodeRepository zipCodeRepository,
            IImageHelper imageHelper,
            ICityRepository cityRepository)
        {
            _userHelper = userHelper;
            _converterHelper = converterHelper;
            _mailHelper = mailHelper;
            _dataInputHelper = dataInputHelper;
            _zipCodeRepository = zipCodeRepository;
            _imageHelper = imageHelper;
            _cityRepository = cityRepository;
        }




        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Main", "Home");
            }

            return this.View();
        }





        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {


                var result = await _userHelper.LoginAsync(model);

                if (result.Succeeded)
                {

                    var user = await _userHelper.GetUserByEmailAsync(model.UserName);
                    if (user.IsActive == false)
                    {
                        TempData["UserId"] = user.Id;
                        return this.RedirectToAction("UpdateUser", "Accounts");
                    }
                    

                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }


                    return this.RedirectToAction("Main", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to login");
            //ViewData["Partial"] = "None";
            return this.View(model);
        }






        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogOutAsync();
            return RedirectToAction("Index", "Home");
        }






        public IActionResult Register()
        {

            var model = new RegisterViewModel();

            return View(model);
        }






        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.UserName);

                if (user == null)
                {

                    user = _converterHelper.ToNewUserFromRegisterViewModel(model);

                    user.IsActive = false;
                    user.CreationDate = DateTime.UtcNow;
                    

                    var result = await _userHelper.AddUserAsync(user, model.Password);



                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user could not be created");
                        return this.View(model);
                    }

                    var UserRole = await _userHelper.IsUSerInRoleAsync(user, "Customer");

                    if (!UserRole)
                    {
                        await _userHelper.AddUserToRoleAsync(user, "Customer");
                    }

                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);

                    var tokenLink = this.Url.Action("ConfirmEmail", "Accounts", new
                    {
                        userId = user.Id,
                        token = myToken,
                    }, protocol: HttpContext.Request.Scheme);


                    _mailHelper.SendEmail(model.UserName, "Email Confirmation", $"<h1>Email Confirmation</h1>" +
                      $"To allow the user, " +
                      $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");


                    this.ViewBag.Message = "The instructions to complete the registration has been sent to your email";


                    return View();
                }

                this.ModelState.AddModelError(string.Empty, "The user already exists");
            }

            return this.View(model);
        }







        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return NotFound();
            }

           
            ViewBag.User = user.UserName;


            return View(user);
        }





        [HttpPost]
        public IActionResult ConfirmEmail(User user)
        {
            if (ModelState.IsValid)
            {

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to performe this action, please try again later");
                    return View(user);
                }


                //var model = _converterHelper.ToUpdateDataViewModel(user);

                TempData["UserId"] = user.Id;
                return RedirectToAction("UpdateUser", "Accounts");
            }

            return View(user);
        }






        public async Task<IActionResult> UpdateUser()
        {

            var userId = (string)TempData["UserId"];

            var user = await _userHelper.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            ViewBag.User = user.UserName;

            var zipCode = await _zipCodeRepository.GetByIdAsync(user.ZipCodeId);

            var model = _converterHelper.ToUpdateDataViewModel(user, zipCode);

            return View(model);
            
        }





        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByIdAsync(model.User.Id);

                var zipCodeId = await _zipCodeRepository.GetZipCodeAsync(model.ZipCode4, model.ZipCode3);


                if (zipCodeId == null)
                {
                    var zip = await _zipCodeRepository.GetCityIdFromZip4(model.ZipCode4);

                    if (zip == null)
                    {
                        ModelState.AddModelError(string.Empty, "The first 4 numbers of zip code are not valid, please insert again");
                        return View(model);
                    }

                    var newZipCode = _converterHelper.ToNewZipCode(model.ZipCode4, model.ZipCode3, zip.CityId);

                    await _zipCodeRepository.CreateAsync(newZipCode);


                    var pathZipNull = string.Empty;

                    if (model.ImageFile != null)
                    {
                        pathZipNull = await _imageHelper.UploadImageAsync(model.ImageFile, "Users");
                    }

                    var zipCodeIdNew = await _zipCodeRepository.GetZipCodeAsync(model.ZipCode4, model.ZipCode3);

                    var updateUserZipNull = _converterHelper.ToUserFromUpdate(model, user, zipCodeIdNew.Id, pathZipNull);


                    if (updateUserZipNull == null)
                    {
                        ModelState.AddModelError(string.Empty, "User not found, please try again");
                        return View(model);
                    }

                    updateUserZipNull.IsActive = true;
                    var resultZipNull = await _userHelper.UpdateUserAsync(updateUserZipNull);

                    if (resultZipNull.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }




                var path = string.Empty;

                if (model.ImageFile!=null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Users");
                }


                var updateUser = _converterHelper.ToUserFromUpdate(model, user, zipCodeId.Id, path);



                if (updateUser==null)
                {
                    ModelState.AddModelError(string.Empty, "User not found, please try again");
                    return View(model);
                }

                updateUser.IsActive = true;
                var result = await _userHelper.UpdateUserAsync(updateUser);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }






        public IActionResult RecoverPassword()
        {
            var model = new RecoverPasswordViewModel();

            return View(model);

        }



        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);

                if (user == null)
                {

                    ViewBag.Message = $"There is no user registered with the email {model.Email}, register, or chose another";
                    return View();
                }

                var tokenPass = await _userHelper.GenerateResetPasswordTokenAsync(user);
                var tokenMail = await _userHelper.GenerateEmailConfirmationTokenAsync(user);

                var tokenLink = this.Url.Action("ResetPassword", "Accounts", new
                {
                    userId = user.Id,
                    token = tokenMail,
                    tokenPass = tokenPass,
                }, protocol: HttpContext.Request.Scheme);


                _mailHelper.SendEmail(model.Email, "Recover Password", $"<h1>Recover Password</h1>" +
                  $"To recover your password, " +
                  $"plase click in this link:</br></br><a href = \"{tokenLink}\">Recover Password</a>");


                this.ViewBag.Message = "The instructions to recover your password has been sent to your email";


                return View();

            }

            return View(model);
        }



        public async Task<IActionResult> ResetPassword (string userId, string token, string tokenPass)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(userId);

            var result = await _userHelper.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return NotFound();
            }

            var model = _converterHelper.ToResetPasswordViewModel(user);


            TempData["Token"] = tokenPass;
            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _converterHelper.ToUserFromResetPasswordViewModel(model);

                if (user==null)
                {
                    return NotFound();
                }

                string token = TempData["token"].ToString();
                string newPassword = model.NewPassword;

                var result = await _userHelper.ResetPasswordAsync(user, token, newPassword);

                if (result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Your password has been changed, please do login");
                    return View();
                }

            }

            return View(model);
        }


        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userHelper.GetUserByIdAsync(id);

            if (user != null)
            {
                var zipCode = await _zipCodeRepository.GetByIdAsync(user.ZipCodeId);
                var model = _converterHelper.ToUpdateDataViewModel(user, zipCode);

                return View(model);
            }
            ViewData["Name"] = "Edit User";
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> EditUser(UpdateUserDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByIdAsync(model.User.Id);

                if (user==null)
                {
                    return NotFound();
                }

                var zipCodeId = await _zipCodeRepository.GetZipCodeAsync(model.ZipCode4, model.ZipCode3);

                var path = string.Empty;

                if (model.User.ImageUrl != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Users");
                }


                var userUpdated = _converterHelper.ToUserFromUpdate(model, user, zipCodeId.Id, path);


                var result = await _userHelper.UpdateUserAsync(userUpdated);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Unable to change your profile at the moment, please try again later");
                    return View(model);
                }

                ViewBag.Message = "Your profile has been updated";
                return View(model);

            }

            return View(model);
        }




        public async Task<IActionResult> ChangePassword()
        {
            var userName = this.User.Identity.Name;

            var user = await _userHelper.GetUserByEmailAsync(userName);

            if (user == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToChangePasswordViewModel(user);

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.UserName);

                if (user == null)
                {
                    return NotFound();
                }

                string oldPassword = model.OldPassword;
                string newPassword = model.NewPassword;
                   

                var checkPassword = await _userHelper.CheckPasswordAsync(user, oldPassword);

                if (!checkPassword)
                {
                    ModelState.AddModelError(string.Empty, "Your Current password is invalid, please try again");
                    return View(model);
                }

                var result = await _userHelper.ChangePasswordAsync(user, oldPassword, newPassword);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Unable to change password, try again later");
                    return View(model);
                }

                @ViewBag.Message = "Password changed successfully";
                return View(model);
            }

            return View(model);
        }



        public async Task<JsonResult> GetZipCodeAndCityId(string zip4, string zip3)
        {
            var exists = await _zipCodeRepository.ZipCodeExistsAsync(zip4, zip3);

            if (exists)
            {
                var zipcode = await _zipCodeRepository.GetZipCodeAsync(zip4, zip3);

                var cityName = await _cityRepository.GetByIdAsync(zipcode.CityId);

                string data = zipcode.Id +"," + cityName.CityName;

                var r = this.Json(data);

                return r;
            }

            return null;
        }
    }
}
