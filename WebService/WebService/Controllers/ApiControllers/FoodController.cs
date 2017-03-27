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
    public class FoodController : ApiController
    {
        private Entities db = new Entities();

        public List<Food> GetAll()
        {
            return db.Foods.ToList();
        }

        // GET: api/Food/5
        [ResponseType(typeof(Food))]
        public IHttpActionResult GetFood(int id)
        {
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return NotFound();
            }

            return Ok(food);
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