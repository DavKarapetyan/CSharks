using CSharks.DAL.Entities;
using CSharks.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Repositories
{
    internal class MessageRepository : IMessageRepository
    {
        private readonly CSharksDbContext _context;
        public MessageRepository(CSharksDbContext context)
        {
            _context = context;
        }
        public void Add(Message message)
        {
            _context.Messages.Add(message);
        }

        public void Delete(int id)
        {
            _context.Messages.Remove(GetById(id));
        }

        public List<Message> GetAll()
        {
            return _context.Messages.ToList();
        }

        public Message GetById(int id)
        {
            var data = _context.Messages.Where(m => m.Id == id).AsNoTracking().FirstOrDefault();
            return data;
        }

        public Message GetForEdit(int id)
        {
            var data = _context.Messages.Where(m => m.Id == id).FirstOrDefault();
            return data;
        }

        public void Update(Message message)
        {
            var data = _context.Messages.FirstOrDefault(m => m.Id == message.Id);
            data.When = message.When;
            data.Text = message.Text;
            data.UserId = message.UserId;
            data.UserName = message.UserName;
        }
    }
}
