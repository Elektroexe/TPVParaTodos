using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    [Table("Zones")]
    public class Zone
    {
        
        [Key]
        public int Id { get; set; }
        public string Location { get; set; }

        public virtual Staff Waiter { get; set; }
        public virtual ICollection<Table> Tables { get; set; }

        public Zone()
        {
            Tables = new List<Table>();
        }
    }
}
