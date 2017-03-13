using Business;
using catipadvalidate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebService.Areas.API.Controllers
{
    public class TableController : JsonController
    {
        private TPVParaTodosEntities db = new TPVParaTodosEntities();

        public JsonResult GetAll()
        {
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}