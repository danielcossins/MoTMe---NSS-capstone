using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoTMe.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int RecieverId { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public List<int> RecieverIds { get; set; }
    }
}