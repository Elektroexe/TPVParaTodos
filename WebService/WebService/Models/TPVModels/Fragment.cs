using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    [Table("Fragments")]
    public class Fragment
    {
        [Key, Column(Order = 1)]
        public int Product_Id { get; set; }
        [Key, Column(Order = 2)]
        public int Order_Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Product_Id")]
        public virtual Product Product { get; set; }
        [ForeignKey("Order_Id")]
        public virtual Order Order { get; set; }

    }
}