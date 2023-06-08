using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Areas.Admin.Models;
using AuthSystem.Data;
using System.Threading.Tasks;
using System.Collections.Generic; // Add this using directive
using AuthSystem.Areas.Identity.Data;

namespace SeniorProject.Areas.Admin.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController( AuthDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _context = context;
            this.roleManager = roleManager;
        }
        public IActionResult AdminLis()
        {
            return View("~/Views/Home/AdminList.cshtml"); // Render the Donate.cshtml view in the Home folder
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

            return View("~/Views/Home/AdminList.cshtml",adminViewModels);
        }

        //  public IActionResult Create() => View();
        public IActionResult Create()
        {
            return View("~/Views/Home/CreateAdmin.cshtml"); // Render the Donate.cshtml view in the Home folder
        }

        [HttpPost]
        public async Task<IActionResult> Createe(AdminViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
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
            }
            
            return RedirectToAction("AdminList");
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminViewModel model, string newRole = "Admin")
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // User not found, handle the error
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            // Remove the user from their current roles
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                // Removing roles failed, handle the error
                return BadRequest();
            }

            // Add the user to the new role
            var addResult = await _userManager.AddToRoleAsync(user, newRole);
            if (!addResult.Succeeded)
            {
                // Adding role failed, handle the error
                return BadRequest();
            }

            return RedirectToAction("AdminList");
        }
        



        // Other actions...
    }
}