using SeniorProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using SeniorProject.Services; // Added this line

namespace SeniorProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> UserManager { get; set; }
        private SignInManager<IdentityUser> SignInManager { get; set; }
        private readonly EmailSender _emailSender; // Added this line

        public AccountController(UserManager<IdentityUser> UserManager, SignInManager<IdentityUser> SignInManager, EmailSender emailSender) // Modified this line
        {
            this.UserManager = UserManager;
            this.SignInManager = SignInManager;
            _emailSender = emailSender; // Added this line
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
                IdentityUser identityUser = new IdentityUser();
                identityUser.UserName = model.Email;
                IdentityResult identityResult = await UserManager.CreateAsync(identityUser, model.Password);
                if (identityResult.Succeeded)
                {
                    // Send a welcome email to the user - Added this block
                    string subject = "Welcome to Earth Care Initiative";
                    string message = "Thank you for joining Earth Care Initiative! We are excited to have you on board.";
                    await _emailSender.SendEmailAsync(model.Email, subject, message);

                    await SignInManager.SignInAsync(identityUser, isPersistent: false);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }

        [Route("[area]/[controller]/[action]")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult identityResult = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid Username/Password");
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}