using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    [Table("Tables")]
    public class Table
    {
        [Key]
        public int Id { get; set; }
        public int MaxPeople { get; set; }
        public bool Empty { get; set; }

        public virtual Zone Zone { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public Table()
        {
            Orders = new List<Order>();
        }
    }
}
