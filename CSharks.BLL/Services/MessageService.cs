using CSharks.BLL.Services.Interfaces;
using CSharks.BLL.ViewModels;
using CSharks.DAL.Entities;
using CSharks.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharks.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;
        public MessageService(IMessageRepository messageRepository, IUnitOfWork unitOfWork)
        {
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
        }
        public void Add(MessageVM model)
        {
            var message = new Message()
            {
                Text = model.Text,
                UserId = model.UserId,
                UserName = model.UserName,
                When = model.When,
            };
            _messageRepository.Add(message);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _messageRepository.Delete(id);
            _unitOfWork.Save();
        }

        public List<MessageVM> GetAll()
        {
            var data = _messageRepository.GetAll().Select(m => new MessageVM()
            {
                Id = m.Id,
                UserName = m.UserName,
                When = m.When,
                Text = m.Text,
                UserId = m.UserId
            }).ToList();
            return data;
        }

        public MessageVM GetById(int id)
        {
            var data = _messageRepository.GetById(id);
            var message = new MessageVM()
            {
                Id = data.Id,
                Text = data.Text,
                UserId = data.UserId,
                UserName = data.UserName,
                When = data.When,
            };
            return message;
        }

        public MessageVM GetForEdit(int id)
        {
            var data = _messageRepository.GetForEdit(id);
            var message = new MessageVM()
            {
                Id = data.Id,
                Text = data.Text,
                UserId = data.UserId,
                UserName = data.UserName,
                When = data.When,
            };
            return message;
        }

        public void Update(MessageVM model)
        {
            var data = _messageRepository.GetForEdit(model.Id);
            data.Text = model.Text;
            data.When = model.When;
            data.UserId = model.UserId;
            data.UserName = model.UserName;

            _messageRepository.Update(data);
            _unitOfWork.Save();
        }
    }
}
