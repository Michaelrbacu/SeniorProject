using EmailDatabase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace EmailDatabase.Areas.Admin.Controllers
{


    [Authorize(Roles = "Admin")]

    [Area("Admin")]
    public class EmailController : Controller
    {
        private EmailContext emailContext { get; set; }

        public EmailController(EmailContext emailContext)
        {
            this.emailContext = emailContext;
        }
        [Route("[area]/[controller]/[action]")]
        [HttpGet]
        public IActionResult Add()
        {
            ViewData["AddEdit"] = "Add";
            return View("AddEdit", new Email());
        }
        [Route("[area]/[controller]/[action]")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Active = "Admin";
            ViewData["AddEdit"] = "Edit";
            Email email = emailContext.Emails.Find(id);
            return View("AddEdit", email);
        }
        [Route("[area]/[controller]/[action]")]
        [HttpPost]
        public IActionResult AddEdit(Email email)
        {
            ViewBag.Active = "Admin";
            if (ModelState.IsValid)
            {
                if (email.EmailId == 0)
                {
                    emailContext.Emails.Add(email);
                }
                else
                {
                    emailContext.Emails.Update(email);
                }
                emailContext.SaveChanges();
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else
            {
                if (email.EmailId == 0)
                {
                    ViewData["AddEdit"] = "Add";
                }
                else
                {
                    ViewData["AddEdit"] = "Edit";
                }
                TempData["message"] = "There was an issue with " + ViewData["AddEdit"]+"ing";
                return View(email);
            }
        }
        [Route("[area]/[controller]/[action]")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Active = "Admin";
            Email email = emailContext.Emails.Find(id);
            return View(email);
        }
        [Route("[area]/[controller]/[action]")]
        [HttpPost]
        public IActionResult Delete(Email email)
        {
            ViewBag.Active = "Admin";
            emailContext.Emails.Remove(email);
            emailContext.SaveChanges();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
