using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Models.DTO;

namespace WebService.Controllers.ApiControllers
{
    public class TableController : ApiController
    {
        private Entities db = new Entities();

        public List<TableDTO> GetAll()
        {
            return db.Tables.ToList().Select(a => new TableDTO(a)).ToList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
