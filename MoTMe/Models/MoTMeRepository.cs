using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
        }

        public void AddMessage(string body, int authorId, int recieverId)
        {
            Message message = new Message();
            message.Body = body;
            message.AuthorId = authorId;
            message.RecieverId = recieverId;
            message.Date = DateTime.Now;
            AddMessage(message);
        }

        public List<Message> GetMessagesByAuthorId_and_ReciverId(int ai, int ri)
        {
            var query = from m in _context.Messages where (m.AuthorId == ai || m.RecieverId == ai) && (m.AuthorId == ri || m.RecieverId == ri) select m;
            return query.ToList<Message>();
        }

        public List<Message> GetMessagesByAuthorAndRecieverId_CertainNumber(int ai, int ri, int number)
        {
            var list = GetMessagesByAuthorId_and_ReciverId(ai, ri);
            var count = list.Count;
            if(count > number)
            {
                return list.GetRange(count - number, number);
            }
            else
            {
                return list;
            }
        }

        public void AddContact(int userId, int contactId)
        {
            Contact contact = new Contact();
            contact.Id = 1;
            contact.UserId = userId;
            contact.ContactId = contactId;
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }

        public List<Contact> GetContactsByUserId(int userId)
        {
            var query = from c in _context.Contacts where c.UserId == userId select c;
            return query.ToList<Contact>();
        }
    }
}