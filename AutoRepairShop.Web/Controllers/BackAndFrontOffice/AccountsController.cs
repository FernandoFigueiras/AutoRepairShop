using AutoRepairShop.Web.Data.Entities;
using AutoRepairShop.Web.Data.Repositories.Interfaces;
using AutoRepairShop.Web.Helpers.Interfaces;
using AutoRepairShop.Web.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
        private readonly IMainWindowConverterHelper _mainWindowConverterHelper;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IComboHelpers _comboHelpers;

        public AccountsController(IUserHelper userHelper,
            IConverterHelper converterHelper,
            IMailHelper mailHelper,
            IDataInputHelper dataInputHelper,
            IZipCodeRepository zipCodeRepository,
            IImageHelper imageHelper,
            ICityRepository cityRepository,
            IMainWindowConverterHelper mainWindowConverterHelper,
            IVehicleRepository vehicleRepository,
            IComboHelpers comboHelpers)
        {
            _userHelper = userHelper;
            _converterHelper = converterHelper;
            _mailHelper = mailHelper;
            _dataInputHelper = dataInputHelper;
            _zipCodeRepository = zipCodeRepository;
            _imageHelper = imageHelper;
            _cityRepository = cityRepository;
            _mainWindowConverterHelper = mainWindowConverterHelper;
            _vehicleRepository = vehicleRepository;
            _comboHelpers = comboHelpers;
        }




        public async Task<IActionResult> Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var userName = this.User.Identity.Name;

                var user = await _userHelper.GetUserByEmailAsync(userName);




                    if (user.IsActive == true)
                    {

                        return this.RedirectToAction("Main", "Home");

                    }
                    else
                    {
                        TempData["UserId"] = user.Id;
                        return this.RedirectToAction("UpdateUser", new { id = user.Id });
                    }
 
               

            }

            return this.View();
        }





        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.UserName);

                if (user==null)
                {
                    this.ModelState.AddModelError(string.Empty, "Failed to login");
                    return this.View(model);
                }
                

                if (user.CanLogin)
                {
                    var result = await _userHelper.LoginAsync(model);

                    if (result.Succeeded)
                    {


                        if (user.IsActive == false)
                        {
                            TempData["UserId"] = user.Id;
                            return this.RedirectToAction("EditUser", new { id = user.Id });
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
                    user.CanLogin = true;

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


                    this.ViewBag.Message = "The instructions for completing your registration have been sent to your email";



                    return View(model);
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


                    var resultZipNull = await _userHelper.UpdateUserAsync(updateUserZipNull);

                    if (resultZipNull.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }




                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Users");
                }


                var updateUser = _converterHelper.ToUserFromUpdate(model, user, zipCodeId.Id, path);



                if (updateUser == null)
                {
                    ModelState.AddModelError(string.Empty, "User not found, please try again");
                    return View(model);
                }

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

                    ViewBag.Message = $"There is no user registered with the email {model.Email}, please try again";
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



        public async Task<IActionResult> ResetPassword(string userId, string token, string tokenPass)
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

                if (user == null)
                {
                    return NotFound();
                }

                string token = TempData["token"].ToString();
                string newPassword = model.NewPassword;

                var result = await _userHelper.ResetPasswordAsync(user, token, newPassword);

                if (result.Succeeded)
                {
                    @ViewBag.UserMessage = "Your password has been changed, open app and perform login";
                    return View();
                }

            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordFromEditUser(UpdateUserDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _converterHelper.ToUserFromEditUserResetPassword(model);

                if (user == null)
                {
                    return NotFound();
                }

                string newPassword = model.NewPassword;

                var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, newPassword);

                if (result.Succeeded)
                {
                    return Redirect($"EditUser/{user.Id}");
                }

            }
            return RedirectToAction("EditUser", new { id = model.User.Id });

        }



        public async Task<IActionResult> EditUser()
        {

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

            if (user != null)
            {
                var vehicles = _vehicleRepository.GetUserVehicles(user.Id);
                var zipCode = await _zipCodeRepository.GetByIdAsync(user.ZipCodeId);
                var model = _converterHelper.ToUpdateDataViewModel(user, zipCode);
                //var userPass = _converterHelper.ToResetPasswordViewModel(user);
                model.User = user;
                model.Vehicles = vehicles.ToList();
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

                if (user == null)
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

                ViewBag.Message = "Changes were applied to your profile";
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

                string data = zipcode.Id + "," + cityName.CityName;

                return this.Json(data);

            }

            return null;
        }


        public async Task<IActionResult> ChangePicture(UpdateUserDataViewModel model)
        {
            if (ModelState.IsValid && model.ImageFile != null)
            {
                var user = await _userHelper.GetUserByIdAsync(model.User.Id);

                if (user != null)
                {
                    if (user.ImageUrl != string.Empty)
                    {
                        _imageHelper.RemovePictureAsync(user.ImageUrl, "users");
                    }

                    var pic = await _imageHelper.UploadImageAsync(model.ImageFile, "Users");

                    user.ImageUrl = pic;
                    await _userHelper.UpdateUserAsync(user);

                    return Redirect($"EditUser/{user.Id}");

                }

            }



            return Redirect($"EditUser/{model.User.Id}");
        }
    }
}
