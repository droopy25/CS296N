using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityWeb.Models
{
    public class MessageViewModel
    {
        public int MessageID { get; set; }
        public string Category { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Date { get; set; }
        public string From { get; set; }
        public Topic TopicName { get; set; }
    }
}