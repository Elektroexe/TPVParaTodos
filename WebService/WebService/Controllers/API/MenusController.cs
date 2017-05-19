using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebService.Models;

namespace WebService.Controllers.API
{
    [Authorize]
    public class MenusController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<MenuDTO> GetMenus()
        {
            return db.Menus.ToList().Select(a => new MenuDTO(a)).ToList();
        }

        public MenuDTO GetMenu(int id)
        {
            return new MenuDTO(db.Menus.FirstOrDefault(a => a.Id == id));
        }
    }
}