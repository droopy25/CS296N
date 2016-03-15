using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityWeb.Models
{
    public class Forum
    {
        public int ForumID { get; set; }
        List<Topic> topics = new List<Topic>();
        public List<Topic> Topics
        {
            get { return topics; }
        }    
    }
}