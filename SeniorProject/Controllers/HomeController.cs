using SeniorProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace SeniorProject.Controllers
{
    public class HomeController : Controller
    {
        private EmailContext emailContext { get; set; }

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

        [HttpPost]
        public IActionResult donations(Donation donation)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Active = "donations";
                emailContext.Donations.Add(donation);
                emailContext.SaveChanges();
                return View();
            }
            ViewBag.Active = "donations";
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}