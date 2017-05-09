using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Commentary { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public int Table_Id { get; set; }

        [ForeignKey("Table_Id")]
        public virtual Table Table { get; set; }
        public virtual ICollection<Fragment> Fragments { get; set; }

        public Order()
        {
            Fragments = new List<Fragment>();
        }
    }
}
