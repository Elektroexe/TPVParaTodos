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
        
    }

    public class NotificationManager
    {
        public static void Notify(Notification notification)
        {
            IHubContext notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            notificationHub.Clients.All.Notify(notification.Title, notification.Message, notification.Type);
        }
    }
}