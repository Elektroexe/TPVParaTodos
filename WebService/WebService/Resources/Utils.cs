using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebService.Models;

namespace WebService.Resources
{
    public class Utils
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
        private static RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        private static ApplicationUserManager aum = new ApplicationUserManager(userStore);

        public static void CreateUser(string username, string email, string password)
        {
            if (aum.FindByEmail(username) == null)
            {
                ApplicationUser newUser = new ApplicationUser()
                {
                    UserName = username,
                    Email = email,
                    EmailConfirmed = true
                };
                aum.Create(newUser, password);
            }
        }

        public static void CreateRole(string rolename)
        {
            if (!roleManager.RoleExists(rolename))
            {
                IdentityRole newRole = new IdentityRole();
                newRole.Name = rolename;
                roleManager.Create(newRole);
            }
        }

        public static void AssignRole(string username, string rolename)
        {
            if (roleManager.RoleExists(rolename) && aum.FindByEmail(username) != null)
            {
                aum.AddToRoleAsync(aum.FindByEmail(username).Id, rolename);
            }
        }

        public static void RemoveRole(string username, string rolename)
        {
            if (roleManager.RoleExists(rolename) && aum.FindByEmail(username) != null)
            {
                aum.RemoveFromRoleAsync(aum.FindByEmail(username).Id, rolename);
            }
        }
    }
}