using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityWeb.Models
{
    public class MessageViewModel
    {
        public int MessageID { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        public string Date { get; set; }
        
        public string From { get; set; }
        public Topic TopicName { get; set; }
        //public Member UserName { get; set; }
    }
}