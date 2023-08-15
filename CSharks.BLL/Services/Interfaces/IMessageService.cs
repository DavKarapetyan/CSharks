using CSharks.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.Services.Interfaces
{
    public interface IMessageService
    {
        void Add(MessageVM model);
        void Update(MessageVM model);
        void Delete(int id);
        MessageVM GetById(int id);
        MessageVM GetForEdit(int id);
        List<MessageVM> GetAll();
    }
}
