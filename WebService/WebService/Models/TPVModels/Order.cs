namespace WebService.Models.TPVModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Drink = new HashSet<Drink>();
            Food = new HashSet<Food>();
            Menu = new HashSet<Menu>();
        }

        public int id { get; set; }

        public int table_id { get; set; }

        public decimal? total { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        [Required]
        [StringLength(128)]
        public string employee_id { get; set; }

        public virtual Table Table { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Drink> Drink { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Food> Food { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Menu> Menu { get; set; }
    }
}
