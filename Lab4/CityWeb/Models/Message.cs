using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityWeb.Models
{
    public class Message
    {
        
        public int MessageID { get; set; }
        public int TopicID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Date { get; set; }
        public string From { get; set; }
    }
}