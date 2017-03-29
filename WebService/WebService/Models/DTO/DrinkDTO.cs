using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models.DTO
{
    public class DrinkDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Commentary { get; set; }
        public Nullable<int> Capacity { get; set; }
        public string TypeBottle { get; set; }
        public Nullable<bool> Soda { get; set; }
        public Nullable<bool> Alcohol { get; set; }

        public DrinkDTO()
        {

        }

        public DrinkDTO(Drink model)
        {
            this.Alcohol = model.Alcohol;
            this.Capacity = model.Capacity;
            this.Commentary = model.Commentary;
            this.Id = model.Id;
            this.Name = model.Name;
            this.Price = model.Price;
            this.Soda = model.Soda;
            this.TypeBottle = model.TypeBottle;
        }
    }
}