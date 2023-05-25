using EmailDatabase.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmailDatabase.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [Route("[area]/[controller]/[action]")]
        public IActionResult Index()
        {
            ViewBag.Active = "Admin";
            List<Email> emails = new List<Email>();
            return View(emails);
        }
    }
}
