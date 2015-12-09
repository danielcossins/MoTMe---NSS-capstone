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
            message.Author = "Billy";
            message.Body = "Hi";
            var now = DateTime.Now;
            message.Date = now;

            Assert.AreEqual(1, message.Id);
            Assert.AreEqual("Billy", message.Author);
            Assert.AreEqual("Hi", message.Body);
            Assert.AreEqual(now, message.Date);
        }
    }
}
