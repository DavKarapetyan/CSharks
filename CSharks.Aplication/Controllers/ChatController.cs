using CSharks.Aplication.Hubs;
using CSharks.BLL.Services.Interfaces;
using CSharks.DAL;
using CSharks.DAL.Entities;
using java.lang;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSharks.Aplication.Controllers
{
    public class ChatController : Controller
    {
        private readonly IMessageService _messageService;
        public ChatController(IMessageService messageService)
        {
            _messageService = messageService;   
        }
        public IActionResult Index()
        {
            var data = _messageService.GetAll();
            return View(data);
        }
    }
}
