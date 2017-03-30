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
using WebService.Models.DTO;

namespace WebService.Controllers.ApiControllers
{
    public class OrderController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Order
        public List<OrderDTO> GetAll()
        {
            return db.Orders.ToList().Select(a => new OrderDTO(a)).ToList();
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

            return Ok(new OrderDTO(order));
        }

        [Route("api/Order/LastByTable/{id}")]
        public OrderDTO GetByTable (int id)
        {
            Order aux = db.Tables.FirstOrDefault(a => a.Id == id).Orders.LastOrDefault();
            return new OrderDTO(aux);
        }

// REFACTOR THIS!!
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
                    Commentary = order.Commentary,
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

            aux.Table.Empty = false;

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

        [Route("api/Order/CloseOrder/{id}")]
        public IHttpActionResult CloseOrder(int id)
        {
            if (ModelState.IsValid)
            {
                Table aux = db.Tables.FirstOrDefault(a => a.Id == id);
                aux.Empty = true;
                db.Entry(aux).State = EntityState.Modified;
                db.SaveChanges();
                return Ok();
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