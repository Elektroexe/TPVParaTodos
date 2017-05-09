using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    [Table("Drinks")]
    public class Drink : Product
    {
        public int Capacity { get; set; }
        public string TypeBottle { get; set; }
        public bool Soda { get; set; }
        public bool Alcohol { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public Drink()
        {
            Menus = new List<Menu>();
            Orders = new List<Order>();
        }
    }
}
