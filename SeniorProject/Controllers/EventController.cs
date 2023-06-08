using AuthSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;
using System.Threading.Tasks;

namespace AuthSystem.Controllers
{
    public class EventController : Controller
    {
        private readonly AuthDbContext _context;

        public EventController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: Event/List
        public async Task<IActionResult> List()
        {
            var events = await _context.Events.ToListAsync();
            return View(events);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,EventStart,EventEnd")] Event events)
        {
            if (ModelState.IsValid)
            {
                _context.Add(events);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(events);
        }

        // ... other action methods ...
    }
}