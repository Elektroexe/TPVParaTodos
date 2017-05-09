using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebService.Hubs;
using WebService.Models;
using WebService.Resources;

namespace WebService.Controllers.API
{
    public class OrdersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("api/Orders/{id}")]
        public OrderDTO Get(int Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Order order = db.Tables.FirstOrDefault(a => a.Id == Id).Orders.LastOrDefault();
                    return new OrderDTO(order);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        [HttpGet]
        [Route("api/Orders/Manager/{tableId}")]
        public OrderManager GetOrderManager(int tableId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Order order = db.Tables.FirstOrDefault(a => a.Id == tableId).Orders.LastOrDefault();
                    OrderManager orderManager = new OrderManager(order);
                    return orderManager;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        [HttpPost]
        [Route("api/Orders/Manager")]
        public IHttpActionResult PostOrderManager([FromBody]OrderManager orderManager)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Order order = orderManager.ToOrder();
                    db.Orders.Add(order);
                    db.SaveChanges();
                    ChangeStatus(orderManager.Table_Id);
                    NotifyRefresh(db.Tables.FirstOrDefault(a => a.Id == orderManager.Table_Id));
                    RefreshTables();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return InternalServerError();
                }
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("api/Orders/Manager")]
        public IHttpActionResult PutOrderManager([FromBody]OrderManager orderManager)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Order oldOrder = db.Orders.FirstOrDefault(a => a.Id == orderManager.Id);
                    Order newOrder = orderManager.ToOrder();
                    db.Entry(oldOrder).CurrentValues.SetValues(newOrder);
                    db.Entry(oldOrder).State = System.Data.Entity.EntityState.Modified;
                    RefreshFragments(oldOrder.Fragments, newOrder.Fragments);
                    db.SaveChanges();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return InternalServerError();
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("api/Orders/Close/{id}")]
        public IHttpActionResult PostCloseOrder(int id)
        {
            ChangeStatus(id);
            NotifyRefresh(db.Tables.FirstOrDefault(a => a.Id == id));
            RefreshTables();
            return Ok();
        }

        private void ChangeStatus(int tableId)
        {
            Table table = db.Tables.FirstOrDefault(a => a.Id == tableId);
            if (table != null)
            {
                table.Empty = !table.Empty;
                db.Entry(table).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        private void RefreshFragments (ICollection<Fragment> oldFragments, ICollection<Fragment> newFragments)
        {
            foreach (Fragment newFragment in newFragments)
            {
                Fragment oldFragment = oldFragments.FirstOrDefault(a => a.Product_Id == newFragment.Product_Id);
                InsertUpdateFragment(oldFragment, newFragment);
            }
            List<Fragment> toRemove = new List<Fragment>();
            foreach (Fragment oldFragment in oldFragments)
            {
                if (!newFragments.Any(a => a.Product_Id == oldFragment.Product_Id))
                {
                    toRemove.Add(oldFragment);
                }
            }
            db.Fragments.RemoveRange(toRemove);
        }

        private void InsertUpdateFragment(Fragment oldFragment, Fragment newFragment)
        {
            if (oldFragment == null)
            {
                db.Fragments.Add(newFragment);
            }
            else
            {
                db.Entry(oldFragment).CurrentValues.SetValues(newFragment);
            }
        }

        private void NotifyRefresh(Table table)
        { 
            Utils.NotifyChange(new Notification
            {
                Title = "Mesa " + table.Id,
                Message = "La mesa " + table.Id + " ha sido " + (table.Empty ? "desocupada" : "ocupada"),
                Type = table.Empty ? Models.Type.Success : Models.Type.Warning
            });
        }

        private void RefreshTables()
        {
            IHubContext tableHub = GlobalHost.ConnectionManager.GetHubContext<TableHub>();
            tableHub.Clients.All.Refresh(db.Tables.ToList().Select(a => new TableDTO(a)).ToJson());
        }
    }
}