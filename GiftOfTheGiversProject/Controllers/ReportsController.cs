using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GiftOfTheGiversProject.Data;
using GiftOfTheGiversProject.Models;
using Microsoft.AspNetCore.Identity;

namespace GiftOfTheGiversProject.Controllers
{
    public class ReportsController : Controller
    {
        private readonly WebsiteDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReportsController(WebsiteDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            var websiteDbContext = _context.ReportProjects.Include(r => r.ReliefProject).Include(r => r.User);
            return View(await websiteDbContext.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.ReportProjects
                .Include(r => r.ReliefProject)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Report_ID == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            ViewData["ReliefProject_ID"] = new SelectList(_context.ReliefProjects, "ReliefProject_ID", "ReliefProject_ID");
            ViewData["User_ID"] = new SelectList(_context.Users, "User_ID", "Email");
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Report_ID,ReliefProject_ID,User_ID,Title,Location,Description,Date")] Report report)
        {
            var user = await _userManager.GetUserAsync(User);
            var userLoggedin = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            report.User = userLoggedin;
            report.ReliefProject = _context.ReliefProjects.FirstOrDefault(u => u.ReliefProject_ID == report.ReliefProject_ID);

            _context.Add(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.ReportProjects.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["ReliefProject_ID"] = new SelectList(_context.ReliefProjects, "ReliefProject_ID", "ReliefProject_ID", report.ReliefProject_ID);
            ViewData["User_ID"] = new SelectList(_context.Users, "User_ID", "Email", report.User_ID);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Report_ID,ReliefProject_ID,User_ID,Title,Location,Description,Date")] Report report)
        {
            if (id != report.Report_ID)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var userLoggedin = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            report.User = userLoggedin;
            report.ReliefProject = _context.ReliefProjects.FirstOrDefault(u => u.ReliefProject_ID == report.ReliefProject_ID);

            try
            {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Report_ID))
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

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.ReportProjects
                .Include(r => r.ReliefProject)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Report_ID == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.ReportProjects.FindAsync(id);
            if (report != null)
            {
                _context.ReportProjects.Remove(report);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.ReportProjects.Any(e => e.Report_ID == id);
        }
    }
}
