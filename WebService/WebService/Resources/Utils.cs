using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebService.Hubs;
using WebService.Models;

namespace WebService.Resources
{
    public class Utils
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
        private static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        private static ApplicationUserManager aum = new ApplicationUserManager(userStore);

        public static async Task CreateUser(string username, string password)
        {
            if (aum.FindByEmail(username) == null)
            {
                ApplicationUser newUser = new ApplicationUser()
                {
                    UserName = username,
                    Email = username,
                    EmailConfirmed = true
                };
                await aum.CreateAsync(newUser, password);
            }
        }

        public static async Task CreateRole(string rolename)
        {
            if (!roleManager.RoleExists(rolename))
            {
                IdentityRole newRole = new IdentityRole();
                newRole.Name = rolename;
                await roleManager.CreateAsync(newRole);
            }
        }

        public static async Task AssignRole(string username, string rolename)
        {
            if (roleManager.RoleExists(rolename) && aum.FindByEmail(username) != null)
            {
                await aum.AddToRoleAsync(aum.FindByEmail(username).Id, rolename);
            }
        }

        public static async void RemoveRole(string username, string rolename)
        {
            if (roleManager.RoleExists(rolename) && aum.FindByEmail(username) != null)
            {
                await aum.RemoveFromRoleAsync(aum.FindByEmail(username).Id, rolename);
            }
        }

        public static void NotifyChange(Notification notification)
        {
            IHubContext notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            notificationHub.Clients.All.Notify(notification.Title, notification.Message, notification.Type);
        }
    }
}