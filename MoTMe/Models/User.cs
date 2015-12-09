using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoTMe.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }

        public List<Message> Messages { get; set; }
        public List<User> Contacts { get; set; }
    }
}