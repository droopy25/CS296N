using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final.Models
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public int MemberID { get; set; }
        public string Type { get; set; }
        public string Strain { get; set; }
        public decimal Quantity { get; set; }
        public string Q_Type { get; set; }
        public Member MemberName { get; set; }
        public List<State> States { get; set; }
        public SelectList FilteredCity { get; set; }
    }
}