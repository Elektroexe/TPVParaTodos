using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class OrderManager
    {
        public int Id { get; set; }
        public int Table_Id { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public string Commentary { get; set; }
        public List<DrinkManager> Drinks { get; set; }
        public List<FoodManager> Foods { get; set; }
        public List<MenuManager> Menus { get; set; }

        public OrderManager()
        {
            Drinks = new List<DrinkManager>();
            Foods = new List<FoodManager>();
            Menus = new List<MenuManager>();
        }

        public OrderManager(Order model)
        {
            Id = model.Id;
            Commentary = model.Commentary;
            Total = model.Total;
            Date = model.Date;
            Table_Id = model.Table_Id;
            Drinks = model.Fragments.Where(a => a.Product.GetType().Name.Contains("Drink")).Select(a => new DrinkManager(a)).ToList();
            Foods = model.Fragments.Where(a => a.Product.GetType().Name.Contains("Food")).Select(a => new FoodManager(a)).ToList();
            Menus = model.Fragments.Where(a => a.Product.GetType().Name.Contains("Menu")).Select(a => new MenuManager(a)).ToList();
        }  

        public Order ToOrder()
        {
            Order order = new Order()
            {
                Id = Id,
                Commentary = Commentary,
                Date = Date,
                Table_Id = Table_Id,
                Total = Total
            };
            Drinks.ToList().ForEach(a => order.Fragments.Add(a.ToFragment(order)));
            Foods.ToList().ForEach(a => order.Fragments.Add(a.ToFragment(order)));
            Menus.ToList().ForEach(a => order.Fragments.Add(a.ToFragment(order)));
            return order;
        }
    }

    public class MealManager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public double TotalPrice { get; set; }

        public MealManager() { }

        public MealManager(Fragment fragment)
        {
            Id = fragment.Product.Id;
            Name = fragment.Product.Name;
            Quantity = fragment.Quantity;
            Price = (double) fragment.Price;
            Description = fragment.Product.Description;
            TotalPrice = (double) (fragment.Quantity * fragment.Price);

        }

        public Fragment ToFragment(Order order)
        {
            return new Fragment()
            {
                Order = order,
                Order_Id = order.Id,
                Price = (decimal) Price,
                Product_Id = Id,
                Quantity = Quantity
            };
        }
    }

    public class DrinkManager : MealManager
    {
        public int Capacity { get; set; }
        public string TypeBottle { get; set; }
        public bool Soda { get; set; }
        public bool Alcohol { get; set; }

        public DrinkManager() { }

        public DrinkManager(Fragment fragment) : base (fragment)
        {
            Drink drink = fragment.Product as Drink;
            this.Capacity = drink.Capacity;
            this.TypeBottle = drink.TypeBottle;
            this.Soda = drink.Soda;
            this.Alcohol = drink.Alcohol;
        }
    }

    public class FoodManager : MealManager
    {
        public string FamilyDish { get; set; }

        public FoodManager() { }

        public FoodManager(Fragment fragment) : base(fragment)
        {
            Food food = fragment.Product as Food;
            this.FamilyDish = food.FamilyDish;
        }
    }

    public class MenuManager : MealManager
    {
        public short PeopleNumber { get; set; }

        public MenuManager() { }

        public MenuManager(Fragment fragment) : base (fragment)
        {
            Menu menu = fragment.Product as Menu;
            this.PeopleNumber = menu.PeopleNumber;
        }
    }
}