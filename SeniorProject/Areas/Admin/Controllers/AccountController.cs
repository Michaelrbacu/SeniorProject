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
        private readonly EmailService _emailService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, EmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
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

                    // Customize the email message
                    var emailMessage = $"Please confirm your account by <a href='{UrlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

                    await _emailService.SendConfirmationEmailAsync(model.Email, "Confirm your email", emailMessage);

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
                Microsoft.AspNetCore.Identity.SignInResult identityResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);
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
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to find user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmailSuccess" : "ConfirmEmailFailure");
        }

        [HttpGet]
        public IActionResult EmailConfirmationSent()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}