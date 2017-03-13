using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Model
{
    public class TableDTO
    {
        public int id { get; set; }
        public Nullable<int> maxPeople { get; set; }
        public string location { get; set; }
        //public virtual ICollection<Order> Orders { get; set; }
        public bool Empty { get; set; }
    }
}
