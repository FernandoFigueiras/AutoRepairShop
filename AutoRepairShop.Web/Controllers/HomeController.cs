using AutoRepairShop.Web.Helpers.Interfaces;
using AutoRepairShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

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
            }
            return View();
        }


        public async Task<IActionResult> Main()
        {
            var userName = this.User.Identity.Name;

            var user = await _userHelper.GetUserByEmailAsync(userName);

            if (user == null && !this.User.Identity.IsAuthenticated)
            {
                return View();
            }
            else if (user == null && this.User.Identity.IsAuthenticated)
            {
                await _userHelper.LogOutAsync();
                return RedirectToAction("Index");
            }


            return View();
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
