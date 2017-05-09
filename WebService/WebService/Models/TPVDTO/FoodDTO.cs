using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class FoodDTO : ProductDTO
    {
        public string FamilyDish { get; set; }

        public FoodDTO() { }

        public FoodDTO(Food model) : base (model.Id, model.Name, model.Price, model.Description)
        {
            FamilyDish = model.FamilyDish;
        }
    }
}
