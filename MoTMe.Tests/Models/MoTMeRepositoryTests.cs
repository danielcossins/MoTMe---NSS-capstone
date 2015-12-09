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
        //private Mock<DbSet<JitterUser>> mock_set;
        private Mock<DbSet<Message>> mock_message_set;
        private MoTMeRepository repository;

        //private void ConnectMocksToDataStore(IEnumerable<JitterUser> data_store)
        //{
        //    var data_source = data_store.AsQueryable<JitterUser>();
        //    // HINT HINT: var data_source = (data_store as IEnumerable<JitterUser>).AsQueryable();
        //    // Convince LINQ that our Mock DbSet is a (relational) Data store.
        //    mock_set.As<IQueryable<JitterUser>>().Setup(data => data.Provider).Returns(data_source.Provider);
        //    mock_set.As<IQueryable<JitterUser>>().Setup(data => data.Expression).Returns(data_source.Expression);
        //    mock_set.As<IQueryable<JitterUser>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
        //    mock_set.As<IQueryable<JitterUser>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

        //    // This is Stubbing the JitterUsers property getter
        //    mock_context.Setup(a => a.JitterUsers).Returns(mock_set.Object);
        //}

        private void ConnectMocksToDataStore(IEnumerable<Message> data_store)
        {
            var data_source = data_store.AsQueryable<Message>();
            // HINT HINT: var data_source = (data_store as IEnumerable<Jot>).AsQueryable();
            // Convince LINQ that our Mock DbSet is a (relational) Data store.
            mock_message_set.As<IQueryable<Message>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_message_set.As<IQueryable<Message>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_message_set.As<IQueryable<Message>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_message_set.As<IQueryable<Message>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

            // This is Stubbing the Jots property getter
            mock_context.Setup(a => a.Messages).Returns(mock_message_set.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<MoTMeContext>();
            //mock_set = new Mock<DbSet<MoTMeUser>>();
            mock_message_set = new Mock<DbSet<Message>>();
            repository = new MoTMeRepository(mock_context.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            //mock_set = null;
            mock_message_set = null;
            repository = null;
        }

        [TestMethod]
        public void MoTMeContextEnsureICanCreateInstance()
        {
            MoTMeContext context = mock_context.Object;
            Assert.IsNotNull(context);
        }

        [TestMethod]
        public void JitterRepositoryEnsureICanGetAllMessages()
        {
            // Arrange
            var expected = new List<Message>
            {
                new Message {Body = "Hello" },
                new Message { Body = "Hello2"}
            };
            mock_message_set.Object.AddRange(expected);

            ConnectMocksToDataStore(expected);

            // Act
            var actual = repository.GetAllMessages();
            // Assert

            Assert.AreEqual("Hello", actual.First().Body);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
