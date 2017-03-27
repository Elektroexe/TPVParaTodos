using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Model
{
    public class TableDTO
    {
        public int Id { get; set; }
        public int MaxPeople { get; set; }
        public int Zone_id { get; set; }
        //public Zone Zone { get; set; }
        public virtual List<OrderDTO> Orders { get; set; }
        public bool Empty { get; set; }
    }
}
