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
using WebService.Models.DTO;

namespace WebService.Controllers.ApiControllers
{
    public class FoodController : ApiController
    {
        private Entities db = new Entities();

        public List<FoodDTO> GetAll()
        {
            return db.Foods.ToList().Select(a => new FoodDTO(a)).ToList();
        }

        // GET: api/Food/5
        [ResponseType(typeof(FoodDTO))]
        public IHttpActionResult GetFood(int id)
        {
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return NotFound();
            }

            return Ok(new FoodDTO(food));
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