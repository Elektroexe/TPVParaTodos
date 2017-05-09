using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebService.Hubs;
using WebService.Models;
using WebService.Resources;

namespace WebService.Controllers.API
{
    public class TablesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("api/Tables")]
        public List<TableDTO> GetTables()
        {
            return db.Tables.ToList().Select(a => new TableDTO(a)).ToList();
        }

        [HttpGet]
        [Route("api/Tables/Free")]
        public void FreeAll()
        {
            foreach (Table table in db.Tables)
            {
                table.Empty = true;
                db.Entry(table).State = System.Data.Entity.EntityState.Modified;          
            }
            IHubContext contextHub = GlobalHost.ConnectionManager.GetHubContext<TableHub>();
            contextHub.Clients.All.Refresh(db.Tables.ToList().Select(a => new TableDTO(a)).ToJson());
            Utils.NotifyChange(new Notification { Title = "Todas las mesas desocupadas", Message = "WebAPI ha liberado todas las mesas", Type = Models.Type.Success });
            db.SaveChanges();
        }
    }
}