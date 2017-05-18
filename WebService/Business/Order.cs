using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Commentary { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public virtual Table Table { get; set; }
        public virtual ICollection<Drink> Drinks { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }

        public Order()
        {
            Drinks = new List<Drink>();
            Foods = new List<Food>();
            Menus = new List<Menu>();
        }
    }
}
