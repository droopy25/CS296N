using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final.Models
{
    public class MemberViewModel
    {
        public int MemberID { get; set; }
        public int ProductID { get; set; }
        public int CityID { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public City CityName { get; set; }
        public State StateName { get; set; }
        public Product ProductName { get; set; }
    }
}