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
using System.Diagnostics;

namespace WebService.Controllers.ApiControllers
{
    public class OrderController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Order
        public List<Order> GetAll()
        {
            return db.Orders.ToList();
        }

        // GET: api/Order/5
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

        // PUT: api/Order/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Order
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Order aux = new Order
            {
                Date = DateTime.Now,
                Total = order.Total,
                Table = db.Tables.FirstOrDefault(a => a.Id == order.Table_Id)
            };

            List<Drink> drinks = new List<Drink>();
            List<Food> foods = new List<Food>();
            foreach (Drink d in order.Drinks)
            {
                drinks.Add(db.Drinks.FirstOrDefault(a => a.Id == d.Id));
            }

            foreach (Food f in order.Foods)
            {
                foods.Add(db.Foods.FirstOrDefault(a => a.Id == f.Id));
            }

            aux.Drinks = drinks;
            aux.Foods = foods;

            db.Orders.Add(aux);
            try
            {
                db.SaveChanges();
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }

        // DELETE: api/Order/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }
    }
}