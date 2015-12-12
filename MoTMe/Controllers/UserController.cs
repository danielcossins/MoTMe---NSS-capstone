using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace MoTMe.Controllers
{
    public class UserController
    {
        public bool IsLoggedIn()
        {
            try
            {
                Guid UserGuid = (Guid)Membership.GetUser().ProviderUserKey;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetUserId()
        {
            try
            {
                Guid UserGuid = (Guid)Membership.GetUser().ProviderUserKey;
                return UserGuid.ToString();
            }
            catch
            {
                throw new System.Web.HttpException();
            }
        }
    }
}