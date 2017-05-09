using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class FragmentDTO
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        public FragmentDTO() { }

        public FragmentDTO(Fragment model)
        {
            Price = model.Price;
            Quantity = model.Quantity;
            ProductId = model.Product_Id;
            OrderId = model.Order_Id;
        }
    }
}