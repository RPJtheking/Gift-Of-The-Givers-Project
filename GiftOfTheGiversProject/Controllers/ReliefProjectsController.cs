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
    public class ReliefProjectsController : Controller
    {
        private readonly WebsiteDbContext _context;

        public ReliefProjectsController(WebsiteDbContext context)
        {
            _context = context;
        }

        // GET: ReliefProjects
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReliefProjects.ToListAsync());
        }

        // GET: ReliefProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reliefProject = await _context.ReliefProjects
                .FirstOrDefaultAsync(m => m.ReliefProject_ID == id);
            if (reliefProject == null)
            {
                return NotFound();
            }

            return View(reliefProject);
        }

        // GET: ReliefProjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReliefProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReliefProject_ID,ProjectName,Status,Location,StartDate,EndDate")] ReliefProject reliefProject)
        {
            reliefProject.Volunteers = _context.Volunteers.ToList();
            reliefProject.Resources = _context.Resources.ToList();
            reliefProject.Reports = _context.ReportProjects.ToList();
            _context.Add(reliefProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
        }

        // GET: ReliefProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reliefProject = await _context.ReliefProjects.FindAsync(id);
            if (reliefProject == null)
            {
                return NotFound();
            }
            return View(reliefProject);
        }

        // POST: ReliefProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReliefProject_ID,ProjectName,Status,Location,StartDate,EndDate")] ReliefProject reliefProject)
        {
            if (id != reliefProject.ReliefProject_ID)
            {
                return NotFound();
            }

            reliefProject.Volunteers = _context.Volunteers.ToList();
            reliefProject.Resources = _context.Resources.ToList();
            reliefProject.Reports = _context.ReportProjects.ToList();
            try
                {
                    _context.Update(reliefProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReliefProjectExists(reliefProject.ReliefProject_ID))
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

        // GET: ReliefProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reliefProject = await _context.ReliefProjects
                .FirstOrDefaultAsync(m => m.ReliefProject_ID == id);
            if (reliefProject == null)
            {
                return NotFound();
            }

            return View(reliefProject);
        }

        // POST: ReliefProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reliefProject = await _context.ReliefProjects.FindAsync(id);
            if (reliefProject != null)
            {
                _context.ReliefProjects.Remove(reliefProject);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReliefProjectExists(int id)
        {
            return _context.ReliefProjects.Any(e => e.ReliefProject_ID == id);
        }
    }
}
