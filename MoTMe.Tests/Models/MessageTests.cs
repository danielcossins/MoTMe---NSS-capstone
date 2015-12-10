using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoTMe.Models;

namespace MoTMe.Tests.Models
{
    [TestClass]
    public class MessageTests
    {
        [TestMethod]
        public void MessageTestAllComponents()
        {
            Message message = new Message();
            message.Id = 1;
            message.AuthorId = 1;
            message.RecieverId = 2;
            message.Body = "Hi";
            var now = DateTime.Now;
            message.Date = now;

            Assert.AreEqual(1, message.Id);
            Assert.AreEqual(1, message.AuthorId);
            Assert.AreEqual(2, message.RecieverId);
            Assert.AreEqual("Hi", message.Body);
            Assert.AreEqual(now, message.Date);
        }
    }
}
