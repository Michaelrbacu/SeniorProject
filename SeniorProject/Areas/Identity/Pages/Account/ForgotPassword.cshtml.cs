using AuthSystem.Areas.Identity.Data;
using AuthSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SeniorProject.Areas.Identity.EmailService;
//using SeniorProject.Migrations;
using SeniorProject.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SeniorProject.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {

        private AuthDbContext _db;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userInManager;
        private readonly IEmailSender _emailSender;
        private readonly string _baseUrl;

        [BindProperty]
        public ForgotPasswordViewModel forgetPassword { get; set; }
        public ForgotPasswordModel(UserManager<ApplicationUser> um,
       SignInManager<ApplicationUser> sm,
       AuthDbContext db,
       IEmailSender emailSender, IHttpContextAccessor context
       )
        {
            _signInManager = sm;
            _userInManager = um;
            _db = db;
            _emailSender = emailSender;
            var request = context.HttpContext.Request;
            _baseUrl = $"{request.Scheme}://{request.Host}";

        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync(ForgotPasswordViewModel forgotPassword)
        {
            var user = await _userInManager.FindByEmailAsync(forgetPassword.Email);
            if (user!=null)
            {
                var token = await _userInManager.GeneratePasswordResetTokenAsync(user);
                token = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(token));                
                var link = _baseUrl + "/Identity/Account/ChangePassword?" + "token=" + token + "&email=" + user.Email;
                string subject = "Welcome to Earth Care Initiative";
                bool emailResponse = _emailSender.SendEmail(user.Email, subject, link);
                if ( true)
                {
                    return Redirect("/Identity/Account/ForgotPasswordConfirmation");
                    
                }

            }
            else
            {
                ViewData["Message"] = string.Format("This Email " + forgotPassword.Email + " Is Not Registered");

            }


            return Page();

        }
    }
}
