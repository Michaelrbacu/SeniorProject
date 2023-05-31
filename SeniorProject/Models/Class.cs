using AuthSystem.Data;
using Microsoft.AspNetCore.Mvc;

namespace SeniorProject.Models
{
    public class DonationController : Controller
    {
        private readonly AuthDbContext _context;

        public DonationController(AuthDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Donation donation)
        {
            if (ModelState.IsValid)
            {
                donation.Timestamp = DateTime.Now;
                _context.Donations.Add(donation);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Redirect to a page displaying the donations table
            }

            return View(donation);
        }

        public IActionResult Index()
        {
            var donations = _context.Donations.ToList();
            return View(donations);
        }
    }


}
