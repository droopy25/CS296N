using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Member
    {
        public int MemberID { get; set; }
        public int CategoryID { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        List<Product> products = new List<Product>();
        public List<Product> Products
        {
            get { return products; }
        }
    }
}