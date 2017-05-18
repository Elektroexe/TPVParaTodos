using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebService.Models;

namespace WebService.Controllers.API
{
    public class FoodsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<FoodDTO> GetFoods()
        {
            return db.Foods.ToList().Select(a => new FoodDTO(a)).ToList();
        }

        public FoodDTO GetFood(int id)
        {
            return new FoodDTO(db.Foods.FirstOrDefault(a => a.Id == id));
        }
    }
}