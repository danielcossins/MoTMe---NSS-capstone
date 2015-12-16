using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoTMe.Models;
using System.Collections.Generic;
using Moq;
using System.Data.Entity;
using System.Linq;
using MoTMe.Controllers;

namespace MoTMe.Tests.Models
{
    [TestClass]
    public class MoTMeRepositoryTests
    {
        private Mock<MoTMeContext> mock_context;
        private Mock<DbSet<User>> mock_set;
        private Mock<DbSet<Message>> mock_message_set;
        private MoTMeRepository repository;

        private void ConnectMocksToDataStore(IEnumerable<User> data_store)
        {
            var data_source = data_store.AsQueryable<User>();
            // HINT HINT: var data_source = (data_store as IEnumerable<JitterUser>).AsQueryable();
            // Convince LINQ that our Mock DbSet is a (relational) Data store.
            mock_set.As<IQueryable<User>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_set.As<IQueryable<User>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_set.As<IQueryable<User>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_set.As<IQueryable<User>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

            // This is Stubbing the JitterUsers property getter
            mock_context.Setup(a => a.Users).Returns(mock_set.Object);
        }

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
            mock_set = new Mock<DbSet<User>>();
            mock_message_set = new Mock<DbSet<Message>>();
            repository = new MoTMeRepository(mock_context.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            mock_set = null;
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
        public void MoTMeRepositoryEnsureICanGetAllMessages()
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

        [TestMethod]
        public void MoTMeRepositoryEnsureICanGetAllUsers()
        {
            // Arrange
            var expected = new List<User>
            {
                new User { Name = "Billy" },
                new User { Name = "Jimmy"}
            };
            mock_set.Object.AddRange(expected);

            ConnectMocksToDataStore(expected);

            // Act
            var actual = repository.GetAllUsers();
            // Assert

            Assert.AreEqual("Billy", actual.First().Name);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MoTMeRepositoryTestGetUserById()
        {
            // Arrange
            var expected = new List<User>
            {
                new User { Id = 1 },
                new User { Id = 2},
                new User { Id = 3 }
            };
            mock_set.Object.AddRange(expected);

            ConnectMocksToDataStore(expected);
            // Act
            int Id = 2;
            User actual_user = repository.GetUserById(Id);
            // Assert
            Assert.AreEqual(Id, actual_user.Id);
        }

        [TestMethod]
        public void MoTMeRepositoryTestGetMessagesByUserId()
        {
            // Arrange
            var list = new List<Message>
            {
                new Message { AuthorId = 1, Body = "made by 1" },
                new Message { AuthorId = 2, Body = "not made by 1" },
                new Message { AuthorId = 3, Body = "Not made by 1" },
                new Message { AuthorId = 1, Body = "made by 1" }
            };
            mock_message_set.Object.AddRange(list);

            ConnectMocksToDataStore(list);

            List<Message> expectedMessages = new List<Message> { new Message() { AuthorId = 1, Body = "made by 1"}, new Message() { AuthorId = 1, Body = "made by 1"} };
            // Act
            int Id = 1;
            List<Message> messages = repository.GetMessagesByUserId(Id);
            // Assert
            //CollectionAssert.AreEqual(expectedMessages, messages);
            Assert.AreEqual(expectedMessages[0].Body, messages[0].Body);
            Assert.AreEqual(expectedMessages[1].Body, messages[0].Body);
        }

        [TestMethod]
        public void MoTMeRepositoryTestGetMessageById()
        {
            var list = new List<Message>
            {
                new Message { Id = 1, Body = "this is id 1" },
                new Message { Id = 2, Body = "this is id 2" },
                new Message { Id = 3, Body = "this is id 3" },
            };

            mock_message_set.Object.AddRange(list);

            ConnectMocksToDataStore(list);

            var expected = new Message { Id = 2, Body = "this is id 2" };

            // Act
            int Id = 2;
            Message message = repository.GetMessageById(Id);
            var stupid = repository.GetAllMessages();
            // Assert
            Assert.AreEqual(expected.Body, message.Body);
        }

        [TestMethod]
        public void MoTMeRepositoryTestGetUserByUserIdLink()
        {
            // Arrange
            var list = new List<User>
            {
                new User { Id = 1, UserIdLink = "22c54162-9255-49d4-a58f-625a2c9cfb9b" },
                new User { Id = 2, UserIdLink = "63211bb5-e247-4980-80bb-2a1cc9f15208" },
                new User { Id = 3, UserIdLink = "6668a856-71ca-456e-9c29-ca033c44d2f4" }
            };
            mock_set.Object.AddRange(list);

            ConnectMocksToDataStore(list);
            // Act
            string Id = "63211bb5-e247-4980-80bb-2a1cc9f15208";
            User actual_user = repository.GetUserByUserIdLink(Id);

            // Assert
            Assert.AreEqual(2, actual_user.Id);
            Assert.AreEqual(Id, actual_user.UserIdLink);
        }

        //[TestMethod]
        //public void UserControllerTestBothGetUserIdMethods()
        //{
        //    // Arrange
        //    var list = new List<User>
        //    {
        //        new User { Id = 1, UserIdLink = "22c54162-9255-49d4-a58f-625a2c9cfb9b" },
        //        new User { Id = 2, UserIdLink = "63211bb5-e247-4980-80bb-2a1cc9f15208" },
        //        new User { Id = 3, UserIdLink = "6668a856-71ca-456e-9c29-ca033c44d2f4" }
        //    };
        //    mock_set.Object.AddRange(list);

        //    ConnectMocksToDataStore(list);

        //    UserController uc = new UserController();
        //    MoTMeRepository repo = new MoTMeRepository();
        //    var test = repo.GetAllUsers();

        //    Assert.AreEqual(2, uc.GetUserId_Int("63211bb5-e247-4980-80bb-2a1cc9f15208"));
        //}

        [TestMethod]
        public void TestGetMessagesByAuthorId_and_ReciverId()
        {
            var time = DateTime.Now;
            var list = new List<Message>
            {
                new Message { Id = 1, Body = "this is id 1", AuthorId = 1, RecieverId = 2, Date = time },
                new Message { Id = 2, Body = "this is id 2", AuthorId = 1, RecieverId = 3, Date = time },
                new Message { Id = 3, Body = "this is id 3", AuthorId = 1, RecieverId = 2, Date = time },
            };

            mock_message_set.Object.AddRange(list);

            ConnectMocksToDataStore(list);

            var expected = new List<Message>()
            {
                new Message { Id = 1, Body = "this is id 1", AuthorId = 1, RecieverId = 2, Date = time },
                new Message { Id = 3, Body = "this is id 3", AuthorId = 1, RecieverId = 2, Date = time },
            };
            var actual = repository.GetMessagesByAuthorId_and_ReciverId(1, 2);

            Assert.AreEqual(expected[0].Body, actual[0].Body);
            Assert.AreEqual(expected[1].Body, actual[1].Body);

            //MoTMeRepository repo = new MoTMeRepository();
            //var test = repo.GetAllMessages();
        }

        [TestMethod]
        public void TestGetMessagesByAuthorAndRecieverId_CertainNumber()
        {
            var time = DateTime.Now;
            var list = new List<Message>
            {
                new Message { Id = 1, Body = "this is id 1", AuthorId = 1, RecieverId = 2, Date = time },
                new Message { Id = 2, Body = "this is id 2", AuthorId = 1, RecieverId = 3, Date = time },
                new Message { Id = 3, Body = "this is id 3", AuthorId = 1, RecieverId = 2, Date = time },
                new Message { Id = 4, Body = "this is id 4", AuthorId = 1, RecieverId = 2, Date = time },
                new Message { Id = 5, Body = "this is id 5", AuthorId = 1, RecieverId = 2, Date = time },
            };

            mock_message_set.Object.AddRange(list);

            ConnectMocksToDataStore(list);

            var expected = new List<Message>()
            {
                new Message { Id = 4, Body = "this is id 4", AuthorId = 1, RecieverId = 2, Date = time },
                new Message { Id = 5, Body = "this is id 5", AuthorId = 1, RecieverId = 2, Date = time },
            };
            int numberToGet = 2;
            var actual = repository.GetMessagesByAuthorAndRecieverId_CertainNumber(1, 2, numberToGet);

            Assert.AreEqual(expected[0].Id, actual[0].Id);
            Assert.AreEqual(expected[1].Id, actual[1].Id);
        }

        [TestMethod]
        public void TestGetMessagesByAuthorAndRecieverId_CertainNumberWithLessThanTheAmount()
        {
            var time = DateTime.Now;
            var list = new List<Message>
            {
                new Message { Id = 1, Body = "this is id 1", AuthorId = 1, RecieverId = 2, Date = time },
                new Message { Id = 2, Body = "this is id 2", AuthorId = 1, RecieverId = 2, Date = time },
                new Message { Id = 3, Body = "this is id 3", AuthorId = 1, RecieverId = 2, Date = time },
                new Message { Id = 4, Body = "this is id 4", AuthorId = 1, RecieverId = 2, Date = time },
                new Message { Id = 5, Body = "this is id 5", AuthorId = 1, RecieverId = 2, Date = time },
            };

            mock_message_set.Object.AddRange(list);

            ConnectMocksToDataStore(list);

            var expected = new List<Message>()
            {
                new Message { Id = 1, Body = "this is id 1", AuthorId = 1, RecieverId = 2, Date = time },
                new Message { Id = 2, Body = "this is id 2", AuthorId = 1, RecieverId = 2, Date = time },
                new Message { Id = 3, Body = "this is id 3", AuthorId = 1, RecieverId = 2, Date = time },
                new Message { Id = 4, Body = "this is id 4", AuthorId = 1, RecieverId = 2, Date = time },
                new Message { Id = 5, Body = "this is id 5", AuthorId = 1, RecieverId = 2, Date = time },
            };
            int numberToGet = 6;
            var actual = repository.GetMessagesByAuthorAndRecieverId_CertainNumber(1, 2, numberToGet);

            Assert.AreEqual(expected[0].Id, actual[0].Id);
            Assert.AreEqual(expected[1].Id, actual[1].Id);
            Assert.AreEqual(expected[2].Id, actual[2].Id);
            Assert.AreEqual(expected[3].Id, actual[3].Id);
            Assert.AreEqual(expected[4].Id, actual[4].Id);
        }




        //////////////////THESE TESTS PASS, BUT THEY KEEP ADDING TO THE DATABASE VIA repo
        /////////////////Find out how to remove from moq db
        /////////////////
        /////////////////
        //[TestMethod]
        //public void MoTMeRepositoryTestAddMessage()
        //{
        //    var list = new List<Message>
        //    {
        //        new Message { Id = 1, Body = "this is id 1", AuthorId = 1, RecieverId = 2, Date = DateTime.Now },
        //        new Message { Id = 2, Body = "this is id 2", AuthorId = 1, RecieverId = 2, Date = DateTime.Now },
        //        new Message { Id = 3, Body = "this is id 3", AuthorId = 1, RecieverId = 2, Date = DateTime.Now },
        //    };

        //    mock_message_set.Object.AddRange(list);

        //    ConnectMocksToDataStore(list);

        //    Message newMessage = new Message() { Id = 4, Body = "new message", AuthorId = 1, RecieverId = 2, Date = DateTime.Now };
        //    repository.AddMessage(newMessage);
        //    MoTMeRepository repo = new MoTMeRepository();
        //    var test = repo.GetAllMessages();
        //    var test2 = repository.GetAllMessages();

        //    Assert.AreEqual(newMessage.Body, repository.GetMessageById(4).Body);
        //}

        //[TestMethod]
        //public void MoTMeRepositoryTestAddMessageWithParams()
        //{
        //    var list = new List<Message>
        //    {
        //        new Message { Id = 1, Body = "this is id 1", AuthorId = 1, RecieverId = 2, Date = DateTime.Now },
        //        new Message { Id = 2, Body = "this is id 2", AuthorId = 1, RecieverId = 2, Date = DateTime.Now },
        //        new Message { Id = 3, Body = "this is id 3", AuthorId = 1, RecieverId = 2, Date = DateTime.Now },
        //    };

        //    mock_message_set.Object.AddRange(list);

        //    ConnectMocksToDataStore(list);

        //    repository.AddMessage("new message", 1, 2);

        //    Assert.AreEqual("new message", repository.GetMessageById(4).Body);
        //}

        [TestMethod]
        public void TestAddContactToUser()
        {
            int expected = 3;

            repository.AddContact(1, 3);

            var actual = repository.GetContactsByUserId(1);

            Assert.AreEqual(expected, actual);
        }
    }
}
