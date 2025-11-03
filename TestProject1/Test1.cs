using GiftOfTheGiversProject.Controllers;
using GiftOfTheGiversProject.Data;
using GiftOfTheGiversProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TestProject1
{

    // Chat gpt-4 
    
    [TestClass]
    public class ReliefProjectsControllerTests
    {
        private ReliefProjectsController _controller;
        private WebsiteDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            // Use an in-memory database for testing
            var options = new DbContextOptionsBuilder<WebsiteDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new WebsiteDbContext(options);

            // Seed test data
            _context.ReliefProjects.Add(new ReliefProject { ReliefProject_ID = 1, ProjectName = "Flood Relief",Location="Pretoria",Status="Active" });
            _context.SaveChanges();

            _controller = new ReliefProjectsController(_context);
        }

        [TestMethod]
        public async Task Edit_NullId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Edit(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult),
                "Expected NotFoundResult when ID is null.");
        }

        [TestMethod]
        public async Task Edit_InvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Edit(111);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult),
                "Expected NotFoundResult when project is not found.");
        }

        [TestMethod]
        public async Task Edit_ValidId_ReturnsViewWithModel()
        {
            // Act
            var result = await _controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result, "Expected a ViewResult for a valid ID.");
            Assert.IsInstanceOfType(result.Model, typeof(ReliefProject),
                "Expected model to be of type ReliefProject.");

            var model = result.Model as ReliefProject;
            Assert.AreEqual(1, model.ReliefProject_ID, "Expected the correct project to be returned.");
            Assert.AreEqual("Flood Relief", model.ProjectName, "Expected project name to match the seeded data.");
        }

        [TestMethod]
        public async Task Delete_NullId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Delete(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult),
                "Expected NotFoundResult when ID is null.");
        }

        [TestMethod]
        public async Task Delete_InvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.Delete(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult),
                "Expected NotFoundResult when no matching project is found.");
        }

        [TestMethod]
        public async Task Delete_ValidId_ReturnsViewWithModel()
        {
            // Act
            var result = await _controller.Delete(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result, "Expected a ViewResult for a valid project ID.");
            Assert.IsInstanceOfType(result.Model, typeof(ReliefProject),
                "Expected model to be of type ReliefProject.");

            var model = result.Model as ReliefProject;
            Assert.AreEqual(1, model.ReliefProject_ID, "Expected correct project ID to be returned.");
            Assert.AreEqual("Flood Relief", model.ProjectName, "Expected correct project name to be returned.");
        }
    }
}
