using CSharks.Aplication.Hubs;
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
        private readonly CSharksDbContext _context;
        public ChatController(CSharksDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var data = _context.Messages.ToList();
            return View(data);
        }
    }
}
