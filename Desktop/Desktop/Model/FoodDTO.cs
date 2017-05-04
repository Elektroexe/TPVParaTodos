﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Model
{
    public partial class FoodDTO: Meal
    {

        public override int Id { get; set; }
        public override string Name { get; set; }
        public override double Price { get; set; }
        public string FamilyDish { get; set; }
        public string Description { get; set; }

        //public virtual List<MenuDTO> Menus { get; set; }
        //public virtual List<OrderDTO> Orders { get; set; }
    }
}
