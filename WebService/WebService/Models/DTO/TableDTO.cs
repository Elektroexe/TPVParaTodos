using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models.DTO
{
    public class TableDTO
    {
        public int Id { get; set; }
        public int MaxPeople { get; set; }
        public int Zone_Id { get; set; }
        public bool Empty { get; set; }

        public TableDTO()
        {

        }

        public TableDTO(Table model)
        {
            this.Empty = model.Empty;
            this.Id = model.Id;
            this.MaxPeople = model.MaxPeople;
            this.Zone_Id = model.Zone_Id;
        }
    }
}