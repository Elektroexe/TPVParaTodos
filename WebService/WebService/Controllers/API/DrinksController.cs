using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebService.Models;

namespace WebService.Controllers.API
{
    public class DrinksController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<DrinkDTO> GetDrinks()
        {
            return db.Drinks.ToList().Select(a => new DrinkDTO(a)).ToList();
        }

        public DrinkDTO GetDrink(int id)
        {
            return new DrinkDTO(db.Drinks.FirstOrDefault(a => a.Id == id));
        }
    }
}