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
        ManageController mc = new ManageController();

        public User GetUserId_UserObject()
        {
            string idlink = mc.GetUserIdLink();
            return repo.GetUserByUserIdLink(idlink);
        }

        public int GetUserId_Int()
        {
            return GetUserId_UserObject().Id;
        }

        //public List<Message> GetAllUserMessages

        //FOR TESTING
        //public User GetUserId_UserObject(string idlink)
        //{
        //    return repo.GetUserByUserIdLink(idlink);
        //}

        //public int GetUserId_Int(string idlink)
        //{
        //    return GetUserId_UserObject(idlink).Id;
        //}
    }
}