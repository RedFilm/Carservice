using Microsoft.AspNetCore.SignalR;

namespace Carservice.Hubs
{
    public class NotificationHub : Hub
    {
        public Task Send(string name, string message)
        {
            return Clients.Others.SendAsync("Send", message);
        }
    }
}
