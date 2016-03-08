using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Heading = "Welcome to the Growers Connection.";
            ViewBag.Message = "This site is here to help connect Medical Dispensaries to local Growers.";
            return View();
        }
    }
}