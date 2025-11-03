using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GiftOfTheGiversProject.Data;
using Microsoft.AspNetCore.Identity;
using GiftOfTheGiversProject.Models;

namespace GiftOfTheGiversProject.Controllers
{
    public class DonationsController : Controller
    {
        private readonly WebsiteDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DonationsController(WebsiteDbContext context,UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Donations
        public async Task<IActionResult> Index()
        {
            var websiteDbContext = _context.Donation.Include(d => d.User);
            return View(await websiteDbContext.ToListAsync());
        }

        // GET: Donations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donation
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Donation_ID == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // GET: Donations/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Set<GiftOfTheGiversProject.Models.User>(), "User_ID", "Email");
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Donation_ID,UserID,Email,Amount,Date,Status")] GiftOfTheGiversProject.Models.Donation donation)
        {

           
            var user = await _userManager.GetUserAsync(User);
            GiftOfTheGiversProject.Models.User userLoggedin = _context.Users.FirstOrDefault(u => u.Email == user.Email);


            donation.UserID = userLoggedin.User_ID;
            donation.User = userLoggedin;
            donation.Date = DateTime.Now;

            _context.Add(donation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Donations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donation.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Set<GiftOfTheGiversProject.Models.User>(), "User_ID", "Email", donation.UserID);
            return View(donation);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Donation_ID,UserID,Email,Amount,Date,Status")] GiftOfTheGiversProject.Models.Donation donation)
        {
            if (id != donation.Donation_ID)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            GiftOfTheGiversProject.Models.User userLoggedin = _context.Users.FirstOrDefault(u => u.Email == user.Email);


            donation.UserID = userLoggedin.User_ID;
            donation.User = userLoggedin;
            donation.Date = DateTime.Now;
            try
                {
                    _context.Update(donation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationExists(donation.Donation_ID))
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

        // GET: Donations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donation
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Donation_ID == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _context.Donation.FindAsync(id);
            if (donation != null)
            {
                _context.Donation.Remove(donation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationExists(int id)
        {
            return _context.Donation.Any(e => e.Donation_ID == id);
        }
    }
}
