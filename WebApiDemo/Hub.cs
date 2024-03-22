using Microsoft.AspNetCore.SignalR;

namespace WebApiDemo
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string chatName, string senderId, string content)
        {
            await Clients.All.SendAsync("ReceiveMessage", chatName, senderId, content);
        }
    }
}
