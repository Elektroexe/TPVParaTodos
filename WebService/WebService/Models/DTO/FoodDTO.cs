using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models.DTO
{
    public class FoodDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PartOfMenu { get; set; }
        public string FamilyDish { get; set; }
        public string Commentary { get; set; }

        public FoodDTO()
        {

        }

        public FoodDTO(Food model)
        {
            this.Commentary = model.Commentary;
            this.FamilyDish = model.FamilyDish;
            this.Id = model.Id;
            this.Name = model.Name;
            this.PartOfMenu = model.PartOfMenu;
            this.Price = model.Price;
        }
    }
}