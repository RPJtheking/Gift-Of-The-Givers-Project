using GiftOfTheGiversProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public sealed class Test1
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
    }
}
