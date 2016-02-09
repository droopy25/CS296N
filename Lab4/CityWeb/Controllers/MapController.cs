using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityWeb.Controllers
{
    public class MapController : Controller
    {
        // GET: Map
        public ActionResult Map()
        {
            ViewBag.Message = "Here you can look at a map of the City of Springfield";
            ViewBag.Map = "https://www.google.com/maps/@44.0501398,-122.9628424,10577m/data=!3m1!1e3";
            return View();
        }
    }
}