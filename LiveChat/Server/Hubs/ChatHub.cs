using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using LiveChat.Server.Data;
using LiveChat.Shared;

namespace LiveChat.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
        {            
            if(message.Msg.Length > 0)
            {
                message.User = Users.users.GetValueOrDefault(Context.ConnectionId);
                message.Date = DateTime.Now;
                await Clients.All.SendAsync("ReceiveMessage", message);
            }
        }

        public override Task OnConnectedAsync()
        {
            Random rng = new Random();
            string user = $"guest{rng.Next(1, 999999999)}";
            Users.users.Add(Context.ConnectionId, user);
            Clients.Caller.SendAsync("GetUser", user);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Users.users.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
