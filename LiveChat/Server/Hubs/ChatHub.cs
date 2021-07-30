using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using LiveChat.Server.Data;
using LiveChat.Shared;
using LiveChat.Server.Interfaces;

namespace LiveChat.Server.Hubs
{
    public class ChatHub : Hub
    {
        IRandomID rng;
        public ChatHub(IRandomID rng)
        {
            this.rng = rng;
        }
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
            string user = $"guest{rng.CreateID()}";
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
