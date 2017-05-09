using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebService.Models;

namespace WebService.Controllers.API
{
    public class ZonesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<ZoneDTO> GetZones()
        {
            return db.Zones.ToList().Select(a => new ZoneDTO(a)).ToList();
        }

        public ZoneDTO GetZone(int Id)
        {
            return new ZoneDTO(db.Zones.FirstOrDefault(a => a.Id == Id));
        }
    }
}