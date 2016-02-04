using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Heading = "Welcome to my City.";
            ViewBag.Message = "The Purpose of this site is to inform you " +
                "about the city that I live in.";
            ViewBag.Date = DateTime.Now;

            return View();
        }
        public ActionResult About()
        {
            ViewBag.About = "My name is Tony and this site is about my city";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Contact = "You can contact me at Lane Community College.";
            return View();
        }
        public ActionResult History()
        {
            ViewBag.History = "Springfield is located in the southern willamete valley. " +
                "It was founded my the Briggs family in 1848";
            return View();
        }
    }
}