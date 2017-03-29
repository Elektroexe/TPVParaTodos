using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models.DTO
{
    public class MenuDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public short PeopleNumber { get; set; }
        public string Description { get; set; }

        public MenuDTO()
        {

        }

        public MenuDTO(Menu model)
        {
            this.Description = model.Description;
            this.Id = model.Id;
            this.Name = model.Name;
            this.PeopleNumber = model.PeopleNumber;
            this.Price = model.Price;
        }
    }
}