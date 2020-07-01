using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.UniSoft.SignalRHub.Hubs
{
    // Strong typed hub
    
    
    public class StrongTypedMessagesHub : Hub<IMessageClient>
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public Task SendMessage(Message message)
        {
            return Clients.Others.YouHaveGotMessage(message);
        }

        public Task Ping()
        {
            return Clients.Caller.Pong();
        }
    }

    public class MessagesHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;

            // string groupName = Context.User.FindFirst(c => c.Type == "Company").Value;

            Groups.AddToGroupAsync(connectionId, "UniSoft");

            return base.OnConnectedAsync();
        }

                
        public Task SendMessage(Message message)
        {
            // return Clients.All.SendAsync("YouHaveGotMessage", message);
            
            // return Clients.Others.SendAsync("YouHaveGotMessage", message);

            return Clients.Group("UniSoft").SendAsync("YouHaveGotMessage", message);

            // Others = All - Caller
        }

        public Task Ping()
        {
            return Clients.Caller.SendAsync("Pong");
        }
    }
}
