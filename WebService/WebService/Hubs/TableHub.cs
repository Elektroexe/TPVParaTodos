using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using WebService.Models;
using WebService.Resources;

namespace WebService.Hubs
{
    public class TableHub : Hub
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void GetAll()
        {
            Clients.Caller.Refresh(db.Tables.ToList().Select(a => new TableDTO(a)).ToJson());
        }

        public void ChangeStatus(int tableId)
        {
            Table table = db.Tables.FirstOrDefault(a => a.Id == tableId);
            if (table != null)
            {
                table.Empty = !table.Empty;
                db.Entry(table).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            Utils.NotifyChange(new Notification { Title = "Mesa "+tableId, Message = "La mesa " + tableId + " ha sido " + (table.Empty ? "desocupada" : "ocupada"), Type = Models.Type.Success });
            Clients.All.Refresh(db.Tables.ToList().Select(a => new TableDTO(a)).ToJson());
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

    }
}