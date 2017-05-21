using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebService.Models;

namespace WebService.Controllers.API
{
    public class CheckDBController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public dynamic Get(int id)
        {
            List<LogProduct> logs = db.LogsProducts.Where(a => a.Id > id).ToList();
            List<Product> products = logs.Select(a => a.Product).Distinct().ToList();
            return new
            {
                Foods = products.Where(a => a.GetType().Name.Contains("Food")).Select(b => new FoodDTO(b as Food)),
                Drinks = products.Where(a => a.GetType().Name.Contains("Drink")).Select(b => new DrinkDTO(b as Drink)),
                Menus = products.Where(a => a.GetType().Name.Contains("Menu")).Select(b => new MenuDTO(b as Menu)),
                NewVersion = id + logs.Count
            };
        }
    }
}