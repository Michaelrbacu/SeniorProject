using AuthSystem.Areas.Identity.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuthSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger,UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this._userManager = userManager;
        }


       

        [Route("ContactUs")]
        public IActionResult ContactUs()
        {
            ViewBag.Active = "HomeContactUs";
            return View();
        }

        [Route("GetInvolved")]
        public IActionResult GetInvolved()
        {
            ViewBag.Active = "GetInvolved";
            return View();
        }

        [Route("Donation")]
        public IActionResult Donation()
        {
            ViewBag.Active = "HomeDonate";
            return View();
        }
        public IActionResult Index()
        {
            ViewData["UserID"]=_userManager.GetUserId(this.User);
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