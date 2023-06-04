using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Areas.Admin.Models;
using AuthSystem.Data;
using System.Threading.Tasks;
using System.Collections.Generic; // Add this using directive


namespace SeniorProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AuthDbContext _context;

        public AdminController(UserManager<IdentityUser> userManager, AuthDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> AdminList()
        {
            var identityAdmins = await _userManager.GetUsersInRoleAsync("Admin");
            var adminViewModels = new List<AdminViewModel>();

            foreach (var admin in identityAdmins)
            {
                var adminViewModel = new AdminViewModel
                {
                    Id = admin.Id,
                    Email = admin.Email,
                    Name = admin.UserName // Assuming admin.UserName stores the admin's name
                };
                adminViewModels.Add(adminViewModel);
            }

            return View(adminViewModels);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(AdminViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _userManager.AddToRoleAsync(user, "Admin");
            if (result.Succeeded)
            {
                var admin = new AuthSystem.Data.Admin
                {
                    Email = model.Email
                };

                _context.Admin.Add(admin);
                await _context.SaveChangesAsync();

                TempData["message"] = "Admin created successfully!";
            }
            return RedirectToAction("AdminList");
        }

        // Other actions...
    }
}