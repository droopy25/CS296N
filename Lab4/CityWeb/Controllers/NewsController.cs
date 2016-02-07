using CityWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CityWeb.Controllers
{
    public class NewsController : Controller
    {
        Articles things = new Articles();
        public NewsController()
        {
            
            News Current = new News
            {
                Title = "Springfield Clinic in Need of Knitted Hats",
                Article = "http://www.kezi.com/news/366012781.html"
            };
            News Current2 = new News
            {
                Title = "Two Critically Injured in Highway 20 Near Lebanon",
                Article = "http://www.kezi.com/news/Crash_Partially_Blocking_Highway_20_near_Lebanon.html"
            };
            things.Stuff.Add(Current);
            things.Stuff.Add(Current2);          
        }
        // GET: News
        public ActionResult News()
        {            
            ViewBag.Message = "Here you can see top stories";             
            return View();
        }
        public ActionResult Articles()
        {
            //List<News> stuff = things.Stuff[0].News;
            return View();
        }
    }
}