﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Business;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebService.Hubs
{
    public class TableHub : Hub
    {
        private Entities db = new Entities();
        public void GetAll()
        {
            Clients.Caller.Refresh(JsonFrom(db.Tables.ToList()));
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
            Clients.All.Refresh(JsonFrom(db.Tables.ToList()));
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        private string JsonFrom (object data)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, PreserveReferencesHandling = PreserveReferencesHandling.None });
        }
    }

}