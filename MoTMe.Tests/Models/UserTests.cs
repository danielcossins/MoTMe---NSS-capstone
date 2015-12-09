using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoTMe.Models;

namespace MoTMe.Tests.Models
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void UserTestAllComponents()
        {
            User user = new User();
            user.Id = 1;
            user.Name = "Billy";
            user.Phone = 1234567890;

            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("Billy", user.Name);
            Assert.AreEqual(1234567890, user.Phone);
        }
    }
}
