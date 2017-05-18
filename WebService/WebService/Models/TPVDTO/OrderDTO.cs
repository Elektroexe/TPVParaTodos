using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Commentary { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public int TableId { get; set; }
        public List<FragmentDTO> Fragments { get; set; }

        public OrderDTO()
        {
            Fragments = new List<FragmentDTO>();
        }

        public OrderDTO(Order model)
        {
            Id = model.Id;
            Commentary = model.Commentary;
            Total = model.Total;
            Date = model.Date;
            TableId = model.Table_Id;
            Fragments = model.Fragments.ToList().Select(a => new FragmentDTO(a)).ToList();
        }        
    }
}
