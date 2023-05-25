using EmailDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmailDatabase.Controllers
{
    public class HomeController : Controller
    {
        private EmailContext emailContext { get; set; }
        //private readonly ILogger<HomeController> _logger;

        public HomeController(EmailContext emailContext)
        {
            this.emailContext = emailContext;
        }
        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.Active = "HomeIndex";
            List<Email> emails = emailContext.Emails.ToList();
            return View(emails);
        }
        [Route("Privacy")]
        public IActionResult Privacy()
        {
            ViewBag.Active = "HomePrivacy";
            TempData["message"] = "This is from the Privacy Action";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}