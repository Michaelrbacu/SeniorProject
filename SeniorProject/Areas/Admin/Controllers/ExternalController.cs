using SeniorProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SeniorProject.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class ExternalController : Controller
    {
        public IActionResult Challenge(string provider, string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                returnUrl = Url.Content("~/");
            }

            var authenticationProperties = new AuthenticationProperties { RedirectUri = returnUrl };
            return Challenge(authenticationProperties, provider);
        }

        public async Task<IActionResult> Callback()
        {
            // Process the external login callback
            // For example, you can create or update the user in your database based on the external login information and sign them in

            return RedirectToAction("Index", "Home");
        }
    }
}