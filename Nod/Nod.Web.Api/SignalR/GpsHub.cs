using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nod.Web.Api.SignalR
{
    public class GpsHub : Hub
    {
        public void Echo(string message)
        {
            //you're going to configure your client app to listen for this
            Clients.All.SendAsync("Send", message);
        }
    }
}
