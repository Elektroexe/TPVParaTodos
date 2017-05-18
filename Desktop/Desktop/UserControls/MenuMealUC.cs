using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Model;
using Desktop.Controller;

namespace Desktop.UserControls
{
    public class MenuMealUC : MealUC
    {
        private MenuDTO menu;

        public override void SetDTO(Product product)
        {
            menu = (MenuDTO)product;
            this.nameLabel.Text = menu.Name;
            this.qtyLabel.Text = menu.Quantity.ToString(); ;
            this.priceLabel.Text = menu.Price.ToString() + " €";
            this.RepositionLabels();
        }

        public MenuMealUC(MenuDTO menu):base()
        {
            this.menu = menu;
            this.nameLabel.Text = menu.Name;
            this.qtyLabel.Text = menu.Quantity.ToString(); ;
            this.priceLabel.Text = menu.Price.ToString() + " €";
            this.RepositionLabels();
        }

        // TEST
        public MenuMealUC()
        { }
        public override Product AddToOrder()
        {
            this.menu.Quantity += 1;
            if (!AddOrderController.products.Contains(this.menu))
            {
                AddOrderController.products.Add(this.menu);
            }
            else
            {
                modifyQuantity();
            }

            return menu;

        }

        public override Product DelFromOrder()
        {
            if (this.menu.Quantity > 0)
            {
                this.menu.Quantity -= 1;
            }
            if (this.menu.Quantity == 0)
            {
                AddOrderController.products.Remove(this.menu);
            }
            else if (this.menu.Quantity > 0)
            {
                modifyQuantity();
            }

            return menu;
        }

        private void modifyQuantity()
        {
            foreach (Product m in AddOrderController.products)
            {
                if (m.GetType() == typeof(MenuDTO))
                {
                    MenuDTO f = (MenuDTO)m;
                    if (f.Id == this.menu.Id)
                    {
                        f.Quantity = this.menu.Quantity;
                    }
                }
            }
        }
    }
}

