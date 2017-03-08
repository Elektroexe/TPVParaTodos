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
using WebService.Models.TPVModels;

namespace WebService.Controllers
{
    public class DrinksController : ApiController
    {
        private TPVModel db = new TPVModel();

        // GET: api/Drinks
        public IQueryable<Drink> GetDrink()
        {
            return db.Drink;
        }

        // GET: api/Drinks/5
        [ResponseType(typeof(Drink))]
        public IHttpActionResult GetDrink(int id)
        {
            Drink drink = db.Drink.Find(id);
            if (drink == null)
            {
                return NotFound();
            }

            return Ok(drink);
        }

        // PUT: api/Drinks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDrink(int id, Drink drink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != drink.id)
            {
                return BadRequest();
            }

            db.Entry(drink).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinkExists(id))
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

        // POST: api/Drinks
        [ResponseType(typeof(Drink))]
        public IHttpActionResult PostDrink(Drink drink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Drink.Add(drink);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = drink.id }, drink);
        }

        // DELETE: api/Drinks/5
        [ResponseType(typeof(Drink))]
        public IHttpActionResult DeleteDrink(int id)
        {
            Drink drink = db.Drink.Find(id);
            if (drink == null)
            {
                return NotFound();
            }

            db.Drink.Remove(drink);
            db.SaveChanges();

            return Ok(drink);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DrinkExists(int id)
        {
            return db.Drink.Count(e => e.id == id) > 0;
        }
    }
}