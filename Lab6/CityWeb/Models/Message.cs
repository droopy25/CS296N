using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityWeb.Models
{
    public class Message
    {
        
        public int MessageID { get; set; }
        public int TopicID { get; set; }
        //public int MemberID { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        public string Date { get; set; }
        [Required]
        public string From { get; set; }
    }
}