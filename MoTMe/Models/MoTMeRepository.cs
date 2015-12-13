using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MoTMe.Models
{
    public class MoTMeRepository
    {
        private MoTMeContext _context;
        public MoTMeContext Context { get { return _context; } }

        public MoTMeRepository()
        {
            _context = new MoTMeContext();
        }

        public MoTMeRepository(MoTMeContext a_context)
        {
            _context = a_context;
        }

        public List<Message> GetAllMessages()
        {
            var query = from messages in _context.Messages select messages;
            return query.ToList();
        }

        public List<User> GetAllUsers()
        {
            var query = from users in _context.Users select users;
            return query.ToList();
        }

        public User GetUserById(int id)
        {
            var query = from u in _context.Users where u.Id == id select u;
            return query.First<User>();
        }

        public User GetUserByUserIdLink(string idlink)
        {
            var query = from u in _context.Users where u.UserIdLink == idlink select u;
            return query.Single<User>();
        }

        public List<Message> GetMessagesByUserId(int userId)
        {
            var query = from m in _context.Messages where m.AuthorId == userId select m;
            return query.ToList<Message>();
        }

        public Message GetMessageById(int id)
        {
            var query = from m in _context.Messages where m.Id == id select m;
            return query.Single<Message>();
        }
    }
}