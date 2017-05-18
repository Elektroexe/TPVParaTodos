using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Model
{
    public abstract class Product
    {
        private int qty;
        public Product() { }

        [Browsable(false)]
        public virtual int Id { get; set; }

        [DisplayName("Nombre")]
        public virtual string Name {get; set;}

        [DisplayName("Cantidad")]
        public int Quantity {
            get
            {
                return this.qty;
            }
            set
            {
                this.qty = value;
                this.TotalPrice = Price * qty;
            }
        }
        [DisplayName("Subtotal")]
        public double TotalPrice { get; set; }

        [Browsable(false)]
        public virtual double Price { get; set; }
    }
}
