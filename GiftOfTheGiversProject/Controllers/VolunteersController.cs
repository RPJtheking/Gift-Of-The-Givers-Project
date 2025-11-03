using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GiftOfTheGiversProject.Data;
using GiftOfTheGiversProject.Models;

namespace GiftOfTheGiversProject.Controllers
{
    public class VolunteersController : Controller
    {
        private readonly WebsiteDbContext _context;

        public VolunteersController(WebsiteDbContext context)
        {
            _context = context;
        }

        // GET: Volunteers
        public async Task<IActionResult> Index()
        {
            var websiteDbContext = _context.Volunteers.Include(v => v.ReliefProject).Include(v => v.User);
            return View(await websiteDbContext.ToListAsync());
        }

        // GET: Volunteers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers
                .Include(v => v.ReliefProject)
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Volunteer_ID == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        // GET: Volunteers/Create
        public IActionResult Create()
        {
            ViewData["Project_ID"] = new SelectList(_context.ReliefProjects, "ReliefProject_ID", "ReliefProject_ID");
            ViewData["User_ID"] = new SelectList(_context.Users, "User_ID", "Email");
            return View();
        }

        // POST: Volunteers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Volunteer_ID,User_ID,Username,Project_ID,Availability")] Volunteer volunteer)
        {
            var user = _context.Users.Find(volunteer.User_ID);
            var project = _context.ReliefProjects.Find(volunteer.Project_ID);

            volunteer.User = user;
            volunteer.ReliefProject = project;

            _context.Add(volunteer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Volunteers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer == null)
            {
                return NotFound();
            }
            ViewData["Project_ID"] = new SelectList(_context.ReliefProjects, "ReliefProject_ID", "ReliefProject_ID", volunteer.Project_ID);
            ViewData["User_ID"] = new SelectList(_context.Users, "User_ID", "Email", volunteer.User_ID);
            return View(volunteer);
        }

        // POST: Volunteers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Volunteer_ID,User_ID,Username,Project_ID,Availability")] Volunteer volunteer)
        {
            if (id != volunteer.Volunteer_ID)
            {
                return NotFound();
            }

            var user = _context.Users.Find(volunteer.User_ID);
            var project = _context.ReliefProjects.Find(volunteer.Project_ID);

            volunteer.User = user;
            volunteer.ReliefProject = project;
            try
                {
                    _context.Update(volunteer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolunteerExists(volunteer.Volunteer_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Volunteers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers
                .Include(v => v.ReliefProject)
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Volunteer_ID == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        // POST: Volunteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer != null)
            {
                _context.Volunteers.Remove(volunteer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolunteerExists(int id)
        {
            return _context.Volunteers.Any(e => e.Volunteer_ID == id);
        }
    }
}
