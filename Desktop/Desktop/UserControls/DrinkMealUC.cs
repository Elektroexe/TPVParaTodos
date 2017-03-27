using Desktop.Model;
using Desktop.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.UserControls
{
    public class DrinkMealUC: MealUC
    {
        private DrinkDTO drink;
        public DrinkMealUC(DrinkDTO drink):base()
        {
            this.drink = drink;
            this.nameLabel.Text = drink.Name;
            this.priceLabel.Text = drink.Price.ToString() + " €";
            this.RepositionLabels();
        }

        public override Meal AddToOrder()
        {
            this.drink.Quantity += 1;
            if (!AddOrderController.meals.Contains(this.drink))
            {
                AddOrderController.meals.Add(this.drink);
            }
            else
            {
                modifyQuantity();
            }

            return drink;
        }

        public override Meal DelFromOrder()
        {
            if (this.drink.Quantity > 0)
            {
                this.drink.Quantity -= 1;
            }
            if (this.drink.Quantity == 0)
            {
                AddOrderController.meals.Remove(this.drink);
            }
            else if (this.drink.Quantity > 0)
            {
                modifyQuantity();
            }

            return drink;
        }

        private void modifyQuantity()
        {
            foreach (Meal m in AddOrderController.meals)
            {
                if (m.GetType() == typeof(DrinkDTO))
                {
                    DrinkDTO d = (DrinkDTO)m;
                    if (d.Id == this.drink.Id)
                    {
                        d.Quantity = this.drink.Quantity;
                    }
                }
            }
        }
    }
}
