using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoRepairShop.Web.Models;
using AutoRepairShop.Web.Helpers.Interfaces;

namespace AutoRepairShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMainWindowConverterHelper _mainWindowConverterHelper;
        private readonly IUserHelper _userHelper;

        public HomeController(IMainWindowConverterHelper mainWindowConverterHelper,
            IUserHelper userHelper)
        {
            _mainWindowConverterHelper = mainWindowConverterHelper;
            _userHelper = userHelper;
        }


        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Main", "Home");
                //return Redirect("~/Home/Main");


            }
            return View();
        }


        public async Task<IActionResult> Main()
        {
            var userName = this.User.Identity.Name;

            var user = await _userHelper.GetUserByEmailAsync(userName);

            if (user == null)
            {
                return View();
            }
            var model = _mainWindowConverterHelper.ToMainWindowViewModel(user);

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

     
    }
}
