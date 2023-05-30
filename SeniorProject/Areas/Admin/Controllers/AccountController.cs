using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models;
using System.Text.Encodings.Web;
using SeniorProject.Areas.Admin.Controllers;
using AuthSystem.Areas.Identity.Data; // Import the namespace for ApplicationUser

namespace SeniorProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly EmailSender _emailSender; // Change this line

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, EmailSender emailSender) // Change this line
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender; // Change this line
        }

        [Route("[area]/[controller]/[action]")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser identityUser = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                IdentityResult identityResult = await _userManager.CreateAsync(identityUser, model.Password);
                if (identityResult.Succeeded)
                {
                    // Generate email confirmation token and send it
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = identityUser.Id, code = code }, protocol: HttpContext.Request.Scheme);

                    // Customize the email subject and message
                    string emailSubject = "Welcome to EarthCare";
                    string emailMessage = $"<h1>Welcome to EarthCare</h1><p>Thank you for joining our community! Please confirm your account by <a href='{UrlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.</p>";

                    await _emailSender.SendEmailAsync(model.Email, emailSubject, emailMessage); // Change this line

                    // Redirect the user to the email confirmation sent page
                    return RedirectToAction("EmailConfirmationSent");
                }

                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // The rest of the controller code remains the same
        // ...
    }
}