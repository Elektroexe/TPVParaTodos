using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class ZoneDTO
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public List<TableDTO> Tables { get; set; }

        public ZoneDTO()
        {
            Tables = new List<TableDTO>();
        }

        public ZoneDTO(Zone model)
        {
            Id = model.Id;
            Location = model.Location;
            Tables = model.Tables.ToList().Select(a => new TableDTO(a)).ToList();
        }
    }
}
