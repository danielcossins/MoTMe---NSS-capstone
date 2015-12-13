using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using MoTMe.Models;

namespace MoTMe.Controllers
{
    public class UserController
    {
        MoTMeRepository repo = new MoTMeRepository();

        public User GetUserId_UserObject()
        {
            ManageController mc = new ManageController();
            string idlink = mc.GetUserIdLink();
            return repo.GetUserByUserIdLink(idlink);
        }

        public int GetUserId_Int()
        {
            return GetUserId_UserObject().Id;
        }
    }
}