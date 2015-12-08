using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoTMe.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
    }
}