using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Model
{
    public partial class MenuDTO: Product
    {
        public override int Id { get; set; }
        public override string Name { get; set; }
        public override double Price { get; set; }
        public short PeopleNumber { get; set; }
        public string Description { get; set; }

        public virtual List<DrinkDTO> Drinks { get; set; }
        public virtual List<FoodDTO> Foods { get; set; }
        public virtual List<OrderDTO> Orders { get; set; }
    }
}
