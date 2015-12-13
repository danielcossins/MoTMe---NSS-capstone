using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoTMe.Controllers;
using MoTMe.Models;
using Moq;
using System.Web;
using System.Security.Principal;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MoTMe.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        Mock<HttpContextBase> context = new Mock<HttpContextBase>();
        Mock<IIdentity> mockIdentity = new Mock<IIdentity>();
        

        //[TestMethod]
        //public void UserControllerTestBothGetUserIdMethods()
        //{
        //    //context.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
        //    //mockIdentity.Setup(x => x.Name).Returns("test_name");
        //    var mock = new Mock<ControllerContext>();
        //    mock.Setup(p => p.HttpContext.User.Identity.GetUserId()).Returns("63211bb5-e247-4980-80bb-2a1cc9f15208");
        //    var list = new List<User>
        //    {
        //        new User { Id = 1, UserIdLink = "22c54162-9255-49d4-a58f-625a2c9cfb9b" },
        //        new User { Id = 2, UserIdLink = "63211bb5-e247-4980-80bb-2a1cc9f15208" },
        //        new User { Id = 3, UserIdLink = "6668a856-71ca-456e-9c29-ca033c44d2f4" }
        //    };


        //    UserController uc = new UserController();

        //    Assert.AreEqual(2, uc.GetUserId_Int());
        //}
    }
}
