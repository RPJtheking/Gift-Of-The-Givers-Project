using GiftOfTheGiversProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        public void TestProperties()
        {
            User user = new User();
            user.User_ID = 1;
            user.FullName = "Richard";
            user.Email = "Richard@gmail.com";
            Assert.AreEqual(1, user.User_ID);
            Assert.AreEqual("Richard@gmail.com", user.Email);
        }

        [TestMethod]
        public void Donation()
        {
            Donation donation = new Donation();
            donation.Donation_ID = 1;
            donation.Email = "Richard@gmail.com";
            donation.Status = "Active";
            donation.Amount = 100;
            donation.Date = DateTime.Now;

            Assert.AreEqual("Active", donation.Status);
            Assert.AreEqual(100, donation.Amount);
        }

        [TestMethod]
        public void ReliefProject()
        {
            ReliefProject project = new ReliefProject();

            project.StartDate = DateTime.Now;
            project.EndDate = DateTime.Now;
            project.ProjectName = "Test";
            project.Status = "Active";

            Assert.AreEqual("Test", project.ProjectName);
            Assert.AreEqual("Active", project.Status);
        }

        [TestMethod]
        public void Report()
        {
            Report report = new Report();
            report.Report_ID = 1;
            report.User_ID = 1;
            report.Description = "Testing the report object";
            report.Date = DateTime.Now;
            report.User = new User();
            report.Title = "Test Title";

            Assert.AreEqual("Testing the report object", report.Description);
            Assert.AreEqual("Test Title", report.Title);

        }

        [TestMethod]
        public void Resource()
        {
            Resource resource = new Resource();
            resource.Quantity = 1;
            resource.ReliefProject = new ReliefProject();
            resource.Resource_ID = 1;
            resource.Project_ID = 1;

            Assert.AreEqual(1, resource.Quantity);
            Assert.AreEqual(1, resource.Resource_ID);
        }

        [TestMethod]
        public void Volunteer()
        {
            Volunteer volunteer = new Volunteer();
            volunteer.Volunteer_ID = 1;
            volunteer.Availability = "Now";
            volunteer.User = new User();
            volunteer.ReliefProject = new ReliefProject();

            Assert.AreEqual("Now", volunteer.Availability);
            Assert.AreEqual(1, volunteer.Volunteer_ID);
        }
    }
}
