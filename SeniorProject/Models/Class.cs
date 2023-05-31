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

    }


}
