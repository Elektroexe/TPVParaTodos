using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebService.Controllers
{
    public class ImageController : Controller
    {
        public FileResult DrinkImage (int id)
        {
            return File(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\DrinkImages\\" + id +".png", "image/png");
        }

        public FileResult FoodImage (int id)
        {
            return File(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\FoodImages\\" + id + ".png", "image/png");
        }
    }
}