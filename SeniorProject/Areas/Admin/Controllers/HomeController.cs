using AuthSystem.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SeniorProject.Areas.Admin.Models;

namespace SeniorProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AuthDbContext _dbContext;

        public HomeController(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("[area]/[controller]/[action]")]
        public IActionResult Index()
        {
            ViewBag.Active = "Admin";

            // Retrieve the admin emails from your data store
            var adminEmails = _dbContext.Admin.Select(a => new AdminViewModel { Email = a.Email }).ToList();

            // Pass the admin emails to the Portal.cshtml view
            return View("Portal", adminEmails);
        }
    }
}