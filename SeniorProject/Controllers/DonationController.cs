using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models;

namespace SeniorProject.Controllers
{
    public class DonationController : Controller
    {
        private readonly EmailContext _context;

        public DonationController(EmailContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Submit(Donation donation)
        {
            if (ModelState.IsValid)
            {
                _context.Donations.Add(donation);
                _context.SaveChanges();
                return RedirectToAction("Donation");
            }

            return View();
        }

        public IActionResult Donation()
        {
            var donations = _context.Donations.ToList();
            return View(donations);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}