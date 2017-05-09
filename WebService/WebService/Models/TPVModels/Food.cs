using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    [Table("Foods")]
    public class Food : Product
    {
        public string FamilyDish { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }

        public Food()
        {
            Menus = new List<Menu>();
        }
    }

    public class FoodMap : EntityTypeConfiguration<Food>
    {
        public FoodMap()
        {
            ToTable("Foods");
        }
    }
}
