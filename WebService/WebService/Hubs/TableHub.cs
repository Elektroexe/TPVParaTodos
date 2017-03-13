using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Business;
using System.Threading.Tasks;

namespace WebService.Hubs
{
    public class TableHub : Hub
    {
        public void GetAllTables()
        {
            Clients.Caller.RefreshTables(Tables.GetTables());
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }
    }

     static class Tables
    {
        private static List<RestaurantTable> ListTables;

        public static List<RestaurantTable> GetTables()
        {
            if (ListTables == null)
            {
                TPVParaTodosEntities db = new TPVParaTodosEntities();
                ListTables = db.Tables.Select(a => new RestaurantTable
                {
                    Empty = true,
                    id = a.id,
                    location = a.location,
                    maxPeople = a.maxPeople,
                    Orders = a.Orders
                }).ToList();
            }
            return ListTables;
        }
    }

    public class RestaurantTable
    {
        public int id { get; set; }
        public Nullable<int> maxPeople { get; set; }
        public string location { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public bool Empty { get; set; }
    }
}