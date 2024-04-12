using Microsoft.AspNetCore.SignalR;

namespace WebApiDemo
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> logger;

        public async Task AddToChat(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            await Clients.Group(userId).SendAsync("UserAddedToChat", Context.ConnectionId);
        }
        public async Task SendMessage(string UserName, string UserId, string Message)
        {
            await Clients.All.SendAsync("ReceiveMessage", UserName, UserId, Message);
        }
    }
}
