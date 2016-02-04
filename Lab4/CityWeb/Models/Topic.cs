using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityWeb.Models
{
    public class Topic
    {
        public int TopicID { get; set; }
        public string Category { get; set; }
        List<Message> messages = new List<Message>();

        public List<Message> Messages
        {
            get { return messages; }
        }
    }
}