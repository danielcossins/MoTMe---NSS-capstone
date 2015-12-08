using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoTMe.Models;
using System.Collections.Generic;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace MoTMe.Tests.Models
{
    [TestClass]
    public class MoTMeRepositoryTests
    {
        private Mock<MoTMeContext> mock_context;
        private Mock<DbSet<Message>> mock_set;

        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}
