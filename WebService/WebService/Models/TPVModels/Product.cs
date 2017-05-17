using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    [Table("Products")]
    public abstract class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public virtual ICollection<Fragment> Fragments { get; set; }
        public virtual ICollection<LogProduct> LogProducts { get; set; }

        public Product()
        {
            Fragments = new List<Fragment>();
            LogProducts = new List<LogProduct>();
        }
    }

    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            HasKey(a => a.Id);
            ToTable("Products");
        }
    }
}
