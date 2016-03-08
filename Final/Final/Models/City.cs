using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final.Models
{
    public class City
    {
        public int CityID { get; set; }
        public int StateID { get; set; }
        public int MemberID { get; set; }
        public string CityName { get; set; }
        public State StateName { get; set; }
        List<Member> members = new List<Member>();
        public List<Member> Members
        {
            get { return members; }
        }
    }
}