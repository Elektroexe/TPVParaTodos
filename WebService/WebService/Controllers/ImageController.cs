using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebService.Controllers
{
    public class ImageController : Controller
    {
        public FileResult Product(int id)
        {
            return File(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\Images\\" + id + ".png", "image/png");
        }
    }
}