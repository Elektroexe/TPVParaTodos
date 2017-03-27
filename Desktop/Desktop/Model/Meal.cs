using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Model
{
    public abstract class Meal
    {
        private int qty;
        public Meal() { }

        public virtual string Name {get; set;}

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

        public double TotalPrice { get; set; }

        [Browsable(false)]
        public virtual double Price { get; set; }
    }
}
