using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    [Table("Foods")]
    public class Food : Product
    {
        public string FamilyDish { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public Food()
        {
            Menus = new List<Menu>();
            Orders = new List<Order>();
        }
    }
}
