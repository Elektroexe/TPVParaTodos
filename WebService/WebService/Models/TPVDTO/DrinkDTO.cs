using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class DrinkDTO : ProductDTO
    {
        public int Capacity { get; set; }
        public string TypeBottle { get; set; }
        public bool Soda { get; set; }
        public bool Alcohol { get; set; }

        public DrinkDTO() { }
        
        public DrinkDTO(Drink model) : base (model.Id, model.Name, model.Price, model.Description, model.Available)
        {
            Capacity = model.Capacity;
            TypeBottle = model.TypeBottle;
            Soda = model.Soda;
            Alcohol = model.Alcohol;
        }

    }
}
