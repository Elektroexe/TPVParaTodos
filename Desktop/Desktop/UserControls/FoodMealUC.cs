using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Controller;
using Desktop.Model;

namespace Desktop.UserControls
{
    public class FoodMealUC: MealUC
    {
        private FoodDTO food;

        public override void SetDTO(Meal meal)
        {
            food = (FoodDTO)meal;
            this.nameLabel.Text = food.Name;
            this.qtyLabel.Text = food.Quantity.ToString(); ;
            this.priceLabel.Text = food.Price.ToString() + " €";
            this.RepositionLabels();
        }

        public FoodMealUC(FoodDTO food):base()
        {
            this.food = food;
            this.nameLabel.Text = food.Name;
            this.qtyLabel.Text = food.Quantity.ToString(); ;
            this.priceLabel.Text = food.Price.ToString() + " €";
            this.RepositionLabels();
        }

        // TEST
        public FoodMealUC()
        {}
        public override Meal AddToOrder()
        {
            this.food.Quantity += 1;
            if (!AddOrderController.meals.Contains(this.food))
            {
                AddOrderController.meals.Add(this.food);
            }
            else
            {
                modifyQuantity();
            }

            return food;

        }

        public override Meal DelFromOrder()
        {
            if (this.food.Quantity > 0)
            {
                this.food.Quantity -= 1;
            }
            if (this.food.Quantity == 0)
            {
                AddOrderController.meals.Remove(this.food);
            }
            else if (this.food.Quantity > 0)
            {
                modifyQuantity();
            }

            return food;
        }

        private void modifyQuantity()
        {
            foreach (Meal m in AddOrderController.meals)
            {
                if (m.GetType() == typeof(FoodDTO))
                {
                    FoodDTO f = (FoodDTO)m;
                    if (f.Id == this.food.Id)
                    {
                        f.Quantity = this.food.Quantity;
                    }
                }
            }
        }
    }
}
