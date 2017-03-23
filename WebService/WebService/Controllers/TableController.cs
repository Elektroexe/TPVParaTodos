using Business;
using catipadvalidate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebService.Controllers
{
    public class TableController : JsonController
    {
        private Entities db = new Entities();
        public JsonResult GetAll()
        {
            var tables = db.Tables.ToList();
            return Json(tables, JsonRequestBehavior.AllowGet);
        }

        public void ChangeEmpty(int tableId)
        {
            Table table = db.Tables.FirstOrDefault(a => a.Id == tableId);
            if (table != null)
            {
                table.Empty = !table.Empty;
                db.Entry(table).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}