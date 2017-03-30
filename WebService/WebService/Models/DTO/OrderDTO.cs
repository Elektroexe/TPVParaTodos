using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public Nullable<int> Table_Id { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Commentary { get; set; }
        public List<FoodDTO> Foods { get; set; }
        public List<DrinkDTO> Drinks { get; set; }
        public List<MenuDTO> Menus { get; set; }

        public OrderDTO()
        {

        }

        public OrderDTO(Order model)
        {
            this.Date = model.Date;
            this.Id = model.Id;
            this.Table_Id = model.Table_Id;
            this.Total = model.Total;
            this.Commentary = model.Commentary;
            this.Foods = model.Foods.ToList().Select(a => new FoodDTO(a)).ToList();
            this.Drinks = model.Drinks.ToList().Select(a => new DrinkDTO(a)).ToList();
            this.Menus = model.Menus.ToList().Select(a => new MenuDTO(a)).ToList();
        }
    }
}