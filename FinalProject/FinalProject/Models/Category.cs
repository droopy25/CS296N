using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public int MemberID { get; set; }
        public string CatType { get; set; }
        List<Member> members = new List<Member>();
        public List<Member> Members
        {
            get { return members; }
        }
    }
}