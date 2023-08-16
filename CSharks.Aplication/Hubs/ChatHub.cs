using CSharks.DAL;
using CSharks.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace CSharks.Aplication.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<User> _userManager;
        private readonly CSharksDbContext _context;

        public ChatHub(UserManager<User> userManager, CSharksDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task SendMessage(string content)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            var sentAt = DateTime.UtcNow;

            var message = new Message
            {
                Text = content,
                UserName = user.UserName,
                UserId = user.Id,
                When = sentAt,
            };
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();


            await Clients.All.SendAsync("ReceiveMessage", user.UserName, content, sentAt.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
