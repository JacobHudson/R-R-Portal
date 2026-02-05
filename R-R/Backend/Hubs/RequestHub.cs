using Microsoft.AspNetCore.SignalR;
using Concierge.Shared;

namespace Backend.Hubs
{
    public class RequestHub : Hub
    {
        public async Task SendRequest(ServiceRequest request)
        {
            await Clients.All.SendAsync("ReceiveRequest", request);
        }
    }
}
