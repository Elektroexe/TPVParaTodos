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
    public abstract class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public ProductDTO() { }

        public ProductDTO(Product model)
        {
            Id = model.Id;
            Name = model.Name;
            Price = model.Price;
            Description = model.Description;
        }

        public ProductDTO(int Id, string Name, decimal Price, string Description)
        {
            this.Id = Id;
            this.Name = Name;
            this.Price = Price;
            this.Description = Description;
        }
    }
}
