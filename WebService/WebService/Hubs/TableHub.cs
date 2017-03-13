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
        public void GetAll()
        {
            Clients.Caller.Refresh(Newtonsoft.Json.JsonConvert.SerializeObject(Tables.GetTables()));
        }

        public void ChangeStatus(int tableId, bool empty)
        {
            Tables.ChangeStatusTable(tableId, empty);
            Clients.All.Refresh(Newtonsoft.Json.JsonConvert.SerializeObject(Tables.GetTables()));
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }
    }

    public static class Tables
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
                    maxPeople = a.maxPeople
                }).ToList();
            }
            return ListTables;
        }

        public static void ChangeStatusTable(int tableId, bool tableStatus)
        {
            int asda = ListTables.Count;
            ListTables[tableId].Empty = tableStatus;
        }

    }

    public class RestaurantTable
    {
        public int id { get; set; }
        public Nullable<int> maxPeople { get; set; }
        public string location { get; set; }
        //public virtual ICollection<Order> Orders { get; set; }
        public bool Empty { get; set; }
    }
}