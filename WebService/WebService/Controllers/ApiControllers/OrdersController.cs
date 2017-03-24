using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Business;

namespace WebService.Controllers.ApiControllers
{
    public class OrdersController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Orders
        public List<Order> GetAll()
        {
            return db.Orders.ToList();
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (ModelState.IsValid){
                Order aux = db.Orders.FirstOrDefault(a => a.Id == id);
                if (aux != null)
                {
                    db.Entry(aux).CurrentValues.SetValues(order);
                    db.SaveChanges();
                }
                return NotFound();
            }
            return BadRequest();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}