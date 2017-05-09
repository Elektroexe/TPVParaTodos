using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebService.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebService.Hubs
{
    public class NotificationHub : Hub
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void NotifyRole(string rolename, string message)
        {
            Clients.Group(rolename).GetNotify(message);
        }

        public override Task OnConnected()
        {
            //if (Context.User.Identity.IsAuthenticated)
            //{
            //    foreach (IdentityRole Role in db.Roles)
            //    {
            //        if (Context.User.IsInRole(Role.Name))
            //        {
            //            Groups.Add(Context.ConnectionId, Role.Name);
            //        }
            //    }
            //}
            return base.OnConnected();
        }
    }
}