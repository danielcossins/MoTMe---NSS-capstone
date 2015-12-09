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
    }
}