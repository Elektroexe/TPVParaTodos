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
using System.Drawing;
using WebService.Models.DTO;

namespace WebService.Controllers.ApiControllers
{
    public class DrinkController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Drink
        public List<DrinkDTO> GetAll()
        {
            return db.Drinks.ToList().Select(a => new DrinkDTO(a)).ToList();
        }

        // GET: api/Drink/5
        [ResponseType(typeof(DrinkDTO))]
        public IHttpActionResult GetDrink(int id)
        {
            Drink drink = db.Drinks.Find(id);
            if (drink == null)
            {
                return NotFound();
            }
            return Ok(new DrinkDTO(drink));
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