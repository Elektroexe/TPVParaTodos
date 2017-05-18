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

        public override void SetDTO(Product product)
        {
            food = (FoodDTO)product;
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
        public override Product AddToOrder()
        {
            this.food.Quantity += 1;
            if (!AddOrderController.products.Contains(this.food))
            {
                AddOrderController.products.Add(this.food);
            }
            else
            {
                modifyQuantity();
            }

            return food;

        }

        public override Product DelFromOrder()
        {
            if (this.food.Quantity > 0)
            {
                this.food.Quantity -= 1;
            }
            if (this.food.Quantity == 0)
            {
                AddOrderController.products.Remove(this.food);
            }
            else if (this.food.Quantity > 0)
            {
                modifyQuantity();
            }

            return food;
        }

        private void modifyQuantity()
        {
            foreach (Product m in AddOrderController.products)
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
