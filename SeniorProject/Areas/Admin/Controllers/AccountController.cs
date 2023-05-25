using EmailDatabase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmailDatabase.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        
        private UserManager<IdentityUser> UserManager { get; set; }
        private SignInManager<IdentityUser> SignInManager { get; set; }


        public AccountController(UserManager<IdentityUser> UserManager, SignInManager<IdentityUser> SignInManager)
        {
            this.UserManager = UserManager;
            this.SignInManager = SignInManager;
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
                identityUser.UserName = model.UserName;
                IdentityResult identityResult = await UserManager.CreateAsync(identityUser, model.Password);
                if(identityResult.Succeeded)
                {
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
            ModelState.AddModelError("","Invalid Username/Password");
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
