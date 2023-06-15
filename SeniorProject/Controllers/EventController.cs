using AuthSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;
using System.Linq;
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
            return View("~/Views/Home/List.cshtml", events);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,EventStart,EventEnd,EventDescription,Registered")] Event events)
        {
            if (ModelState.IsValid)
            {
                _context.Add(events);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(events);
        }

        // GET: Event/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events.FindAsync(id);

            if (events == null)
            {
                return NotFound();
            }
            return View(events);
        }

        // POST: Event/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,EventStart,EventEnd,EventDescription,Registered")] Event events)
        {
            if (id != events.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(events);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(events.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            return View("~/Views/Event/Edit.cshtml", events);
        }

        // POST: Event/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var events = await _context.Events.FindAsync(id);

            if (events == null)
            {
                return Json(new { success = false });
            }

            _context.Events.Remove(events);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // POST: Event/UpdateRegistered
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRegistered(string eventId, string registered)
        {
            if (ModelState.IsValid)
            {
                var eventToUpdate = await _context.Events.FindAsync(eventId);

                if (eventToUpdate == null)
                {
                    return Json(new { success = false });
                }

                eventToUpdate.Registered += ", " + registered;
                _context.Update(eventToUpdate);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        // GET: Event/GetEvents
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events.ToListAsync();
            return Json(events);
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }

    }
}