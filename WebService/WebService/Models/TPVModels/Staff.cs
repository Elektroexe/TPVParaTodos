using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    [Table("Staff")]
    public class Staff
    {
        [Key, ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime LastConnection { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    [Table("Waiters")]
    public class Waiter : Staff
    {
        public virtual ICollection<Zone> Zone { get; set; }
    }

    public class StaffMap : EntityTypeConfiguration<Staff>
    {
        public StaffMap()
        {
            HasKey(a => a.Id);
            ToTable("Staff");
        }
    }

    public class WaiterMap : EntityTypeConfiguration<Waiter>
    {
        public WaiterMap()
        {
            ToTable("Waiters");
        }
    }
}
