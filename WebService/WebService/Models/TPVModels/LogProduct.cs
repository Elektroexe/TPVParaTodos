using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    [Table("LogsProducts")]
    public class LogProduct
    {
        [Key]
        public int Id { get; set; }
        public int Product_Id { get; set; }
        public string Action { get; set; }

        [ForeignKey("Product_Id")]
        public virtual Product Product { get; set; }

    }
}