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

        public override void SetDTO(Product product)
        {
            drink = (DrinkDTO)product;
            this.nameLabel.Text = drink.Name;
            this.priceLabel.Text = drink.Price.ToString() + " €";
            this.qtyLabel.Text = drink.Quantity.ToString();
            this.RepositionLabels();
        }

        public DrinkMealUC(DrinkDTO drink):base()
        {
            this.drink = drink;
            this.nameLabel.Text = drink.Name;
            this.priceLabel.Text = drink.Price.ToString() + " €";
            this.qtyLabel.Text = drink.Quantity.ToString();
            this.RepositionLabels();
        }

        public DrinkMealUC() { }

        public override Product AddToOrder()
        {
            this.drink.Quantity += 1;
            if (!AddOrderController.products.Contains(this.drink))
            {
                AddOrderController.products.Add(this.drink);
            }
            else
            {
                modifyQuantity();
            }

            return drink;
        }


        public override Product DelFromOrder()
        {
            if (this.drink.Quantity > 0)
            {
                this.drink.Quantity -= 1;
            }
            if (this.drink.Quantity == 0)
            {
                AddOrderController.products.Remove(this.drink);
            }
            else if (this.drink.Quantity > 0)
            {
                modifyQuantity();
            }

            return drink;
        }

        private void modifyQuantity()
        {
            foreach (Product m in AddOrderController.products)
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
