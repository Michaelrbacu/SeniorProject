using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;

namespace SeniorProject.Controllers
{
    public class DonationController : Controller
    {
        private readonly AuthDbContext _context;

        public DonationController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: Donation/Create
        public IActionResult Create()
        {
            return View("~/Views/Home/Donate.cshtml"); // Render the Donate.cshtml view in the Home folder
        }

        // POST: Donation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Amount,Timestamp,Address,Phone,Message")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                // Set the current timestamp for the donation
                donation.Timestamp = DateTime.Now;

                _context.Add(donation);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Donated)); // Redirect to the Donated action
            }

            return View("~/Views/Home/Donate.cshtml", donation); // Render the Donate.cshtml view in the Home folder with the invalid donation object
        }

        // GET: Donation/Donated
        public async Task<IActionResult> Donated()
        {
            // Retrieve all the donations from the database
            var donations = await _context.Donations.ToListAsync();

            return View("~/Views/Home/Donated.cshtml", donations); // Render the Donated.cshtml view in the Home folder with the list of donations
        }

        // Additional action methods for Edit, Details, and Delete if needed
    }
}