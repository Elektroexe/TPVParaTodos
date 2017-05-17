using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class MenuDTO : ProductDTO
    {
        public short PeopleNumber { get; set; }

        public MenuDTO() { }
        public MenuDTO(Menu model) : base (model.Id, model.Name, model.Price, model.Description, model.Available)
        {
            PeopleNumber = model.PeopleNumber;
        }
    }
}
