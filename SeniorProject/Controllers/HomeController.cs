using AuthSystem.Areas.Identity.Data;
using AuthSystem.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;
using System.Diagnostics;

namespace AuthSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, AuthDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }

            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();

            return RedirectToAction("Deleted");
        }


        [Route("UserList")]
        public IActionResult UserList()
        {
            ViewBag.Active = "UserList";
            return View();
        }
        [Route("Portal")]
        public IActionResult Portal()
        {
            ViewBag.Active = "Portal";
            return View();
        }

        [Route("ContactUs")]
        public IActionResult ContactUs()
        {
            ViewBag.Active = "HomeContactUs";
            return View();
        }
        [Route("AddEvents")]
        public IActionResult AddEvents()
        {
            ViewBag.Active = "AddEvents";
            return View();
        }
        [Route("EventsList")]
        public IActionResult EventsList()
        {
            ViewBag.Active = "EventsList";
            return View();
        }
        [Route("AdminList")]
        public IActionResult AdminList()
        {
            ViewBag.Active = "AdminList";
            return View();
        }
        [Route("CreateAdmin")]
        public IActionResult CreateAdmin()
        {
            ViewBag.Active = "CreateAdmin";
            return View();
        }

        [Route("GetInvolved")]
        public IActionResult GetInvolved()
        {
            ViewBag.Active = "GetInvolved";
            return View();
        }

        [Route("Donate")]
        public IActionResult Donate()
        {
            ViewBag.Active = "HomeDonate";
            return View();
        }

        [Route("Donated")]
        public IActionResult Donated()
        {
            ViewBag.Active = "HomeDonated";
            return View();
        }

        [HttpPost]
        public IActionResult Create(Donation donation)
        {
            if (ModelState.IsValid)
            {
                // Save the donation to the database
                // Add your database logic here

                return RedirectToAction("Donated");
            }

            // If the model is not valid, redisplay the form with validation errors
            return View("Donated");
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