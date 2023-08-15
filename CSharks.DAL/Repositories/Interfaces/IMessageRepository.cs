using CSharks.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.DAL.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        void Add(Message message);
        void Update(Message message);
        void Delete(int id);
        Message GetById(int id);
        Message GetForEdit(int id);
        List<Message> GetAll();
    }
}
