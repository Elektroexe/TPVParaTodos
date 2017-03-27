using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Model
{
    public partial class OrderDTO
    {        
        public int Id { get; set; }
        public int Table_Id { get; set; }
        public Nullable<decimal> Total { get; set; }
        public System.DateTime Date { get; set; }
        //public string Employee_Id { get; set; }
        public virtual TableDTO Table { get; set; }
        public virtual List<DrinkDTO> Drinks { get; set; }   
        public virtual List<FoodDTO> Foods { get; set; }       
        public virtual List<MenuDTO> Menus { get; set; }
    }
}
