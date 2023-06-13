using AuthSystem.Areas.Identity.Data;
using AuthSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SeniorProject.Models;

namespace SeniorProject.Areas.Identity.Pages.Account
{
    public class ChangePasswordModel : PageModel
    {
        [BindProperty]
        public ForgetPasswordViewModel forgetPassword { get; set; }
        private AuthDbContext _db;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userInManager;

        //public CheckoutCustomer Customer = new CheckoutCustomer();
        //public Basket Basket = new Basket();

        public ChangePasswordModel(UserManager<ApplicationUser> um,
            SignInManager<ApplicationUser> sm,
            AuthDbContext db)
        {
            _signInManager = sm;
            _userInManager = um;
            _db = db;
        }

        public void OnGet([FromQuery] string token, [FromQuery] string email)
        {
            if (token != null && email != null)
            {
                var base64EncodedBytes = System.Convert.FromBase64String(token);
                string str = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                forgetPassword = new ForgetPasswordViewModel();
                forgetPassword.Token = str;
                forgetPassword.Email = email;
            }

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            var user = await _userInManager.FindByEmailAsync(forgetPassword.Email);
            if (user == null)
                return Page();
            var resetPassResult = await _userInManager.ResetPasswordAsync(user, forgetPassword.Token, forgetPassword.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return Page();
            }
            return RedirectToPage("/ResetPasswordConfirmation");

        }
    }
}


