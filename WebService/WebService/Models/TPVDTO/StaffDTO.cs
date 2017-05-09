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
    public class StaffDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime LastConnection { get; set; }

        public StaffDTO() { }
        public StaffDTO(Staff model)
        {
            Id = model.Id;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Address = model.Address;
            Phone = model.Phone;
            LastConnection = model.LastConnection;
        }
        public StaffDTO(string Id, string FirstName, string LastName, string Address, string Phone, DateTime LastConnection)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Address = Address;
            this.Phone = Phone;
            this.LastConnection = LastConnection;
        }
    }
    
    public class WaiterDTO : StaffDTO
    {
        public virtual List<ZoneDTO> Zone { get; set; }

        public WaiterDTO() { }
        public WaiterDTO(Waiter model) : base (model.Id, model.FirstName, model.LastName, model.Address, model.Phone, model.LastConnection)
        {
            Zone = model.Zone.ToList().Select(a => new ZoneDTO(a)).ToList();
        }
    }
}
