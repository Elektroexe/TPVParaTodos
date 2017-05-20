using System;
using System.IO;
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
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\Images\\";
            if (System.IO.File.Exists(path + id + ".png"))
            {
                return File(path + id + ".png", "image/png");
            }
            return File(path + "0.png", "image/png");
        }
    }
}