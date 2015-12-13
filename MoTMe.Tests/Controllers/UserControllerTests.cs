using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoTMe.Controllers;
using MoTMe.Models;
using Moq;
using System.Web;
using System.Security.Principal;
using System.Collections.Generic;
using MoTMe.Models;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;

namespace MoTMe.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        Mock<HttpContextBase> context = new Mock<HttpContextBase>();
        Mock<IIdentity> mockIdentity = new Mock<IIdentity>();

        private Mock<MoTMeContext> mock_context;
        private Mock<DbSet<User>> mock_user;
        private Mock<DbSet<Message>> mock_message;
        private MoTMeRepository repository;

        private void ConnectMocksToDataStore(IEnumerable<User> data_store)
        {
            var data_source = data_store.AsQueryable<User>();
            // HINT HINT: var data_source = (data_store as IEnumerable<JitterUser>).AsQueryable();
            // Convince LINQ that our Mock DbSet is a (relational) Data store.
            mock_user.As<IQueryable<User>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_user.As<IQueryable<User>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_user.As<IQueryable<User>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_user.As<IQueryable<User>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

            // This is Stubbing the JitterUsers property getter
            mock_context.Setup(a => a.Users).Returns(mock_user.Object);
        }

        private void ConnectMocksToDataStore(IEnumerable<Message> data_store)
        {
            var data_source = data_store.AsQueryable<Message>();
            // HINT HINT: var data_source = (data_store as IEnumerable<Jot>).AsQueryable();
            // Convince LINQ that our Mock DbSet is a (relational) Data store.
            mock_message.As<IQueryable<Message>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_message.As<IQueryable<Message>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_message.As<IQueryable<Message>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_message.As<IQueryable<Message>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

            // This is Stubbing the Jots property getter
            mock_context.Setup(a => a.Messages).Returns(mock_message.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<MoTMeContext>();
            mock_user = new Mock<DbSet<User>>();
            mock_message= new Mock<DbSet<Message>>();
            repository = new MoTMeRepository(mock_context.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            mock_user = null;
            mock_message = null;
            repository = null;
        }


        //[TestMethod]
        //public void UserControllerTestBothGetUserIdMethods()
        //{
        //    var list = new List<User>
        //    {
        //        new User { Id = 1, UserIdLink = "22c54162-9255-49d4-a58f-625a2c9cfb9b" },
        //        new User { Id = 2, UserIdLink = "63211bb5-e247-4980-80bb-2a1cc9f15208" },
        //        new User { Id = 3, UserIdLink = "6668a856-71ca-456e-9c29-ca033c44d2f4" }
        //    };
        //    mock_user.Object.AddRange(list);

        //    ConnectMocksToDataStore(list);

        //    UserController uc = new UserController();
        //    MoTMeRepository repo = new MoTMeRepository();
        //    var a = repo.GetAllUsers();

        //    Assert.AreEqual(2, uc.GetUserId_Int("63211bb5-e247-4980-80bb-2a1cc9f15208"));
        //}
    }
}
