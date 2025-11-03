using GiftOfTheGiversProject.Models;
using Microsoft.EntityFrameworkCore;
using WebsiteClassLibrary;

namespace GiftOfTheGiversProject.Data
{
    public class WebsiteDbContext:DbContext
    {
        public WebsiteDbContext(DbContextOptions<WebsiteDbContext> options) : base(options) { }

        public DbSet<GiftOfTheGiversProject.Models. Donation> Donations { get; set; }
        public DbSet<GiftOfTheGiversProject.Models.User> Users { get; set; }
        public DbSet<GiftOfTheGiversProject.Models.ReliefProject> ReliefProjects { get; set; }
        public DbSet<GiftOfTheGiversProject.Models.Report> ReportProjects { get; set; }
        public DbSet<GiftOfTheGiversProject.Models.Volunteer> Volunteers { get; set; }
        public DbSet<GiftOfTheGiversProject.Models.Resource> Resources { get; set; }
        public DbSet<GiftOfTheGiversProject.Models.Donation> Donation { get; set; }
    }
}
