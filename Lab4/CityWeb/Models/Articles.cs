using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityWeb.Models
{
    public class Articles
    {
        public int ArticleID { get; set; }
        List<News> stuff = new List<News>();
        public List<News> Stuff
        {
            get { return stuff; }
        }
    }
}