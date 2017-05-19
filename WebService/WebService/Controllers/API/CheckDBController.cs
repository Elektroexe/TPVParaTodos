using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebService.Models;

namespace WebService.Controllers.API
{
    [Authorize]
    public class CheckDBController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<ProductDTO> Get(int id)
        {
            List<LogProduct> logs = db.LogsProducts.Where(a => a.Id > id).ToList();
            List<ProductDTO> products = new List<ProductDTO>();
            foreach (LogProduct log in logs)
            {
                if (!products.Any(a => a.Id == log.Product_Id)) {
                    products.Add(convertProduct(log.Product));
                }
            }
            return products;
        }

        public ProductDTO convertProduct (Product product)
        {
            if (product.GetType().Name.Contains("Food"))
            {
                 return new FoodDTO(product as Food);
            }
            if (product.GetType().Name.Contains("Drink"))
            {
                return new DrinkDTO(product as Drink);
            }
            return new MenuDTO(product as Menu);
        }
    }
}