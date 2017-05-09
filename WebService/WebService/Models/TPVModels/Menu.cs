using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    [Table("Menus")]
    public class Menu : Product
    {
        public short PeopleNumber { get; set; }

        public virtual ICollection<Drink> Drinks { get; set; }
        public virtual ICollection<Food> Foods { get; set; }

        public Menu()
        {
            Drinks = new List<Drink>();
            Foods = new List<Food>();
        }
    }

    public class MenuMap : EntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            ToTable("Menus");
        }
    }
}
