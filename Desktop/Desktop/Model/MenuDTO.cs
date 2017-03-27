using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Model
{
    public partial class MenuDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public short PeopleNumber { get; set; }
        public string Description { get; set; }

        public virtual List<DrinkDTO> Drinks { get; set; }
        public virtual List<FoodDTO> Foods { get; set; }
        public virtual List<OrderDTO> Orders { get; set; }
    }
}
