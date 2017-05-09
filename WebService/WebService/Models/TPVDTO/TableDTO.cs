using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class TableDTO
    {
        public int Id { get; set; }
        public int MaxPeople { get; set; }
        public bool Empty { get; set; }
        public int ZoneId { get; set; }

        public TableDTO() { }

        public TableDTO(Table model)
        {
            Id = model.Id;
            MaxPeople = model.MaxPeople;
            Empty = model.Empty;
            ZoneId = model.Zone.Id;
        }
    }
}
