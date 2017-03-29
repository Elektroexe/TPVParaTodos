using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Business;
using WebService.Models.DTO;

namespace WebService.Controllers.ApiControllers
{
    public class MenuController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Menu
        public List<MenuDTO> GetAll()
        {
            return db.Menus.ToList().Select(a => new MenuDTO(a)).ToList();
        }

        // GET: api/Menu/5
        [ResponseType(typeof(MenuDTO))]
        public IHttpActionResult GetMenu(int id)
        {
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return NotFound();
            }

            return Ok(new MenuDTO(menu));
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