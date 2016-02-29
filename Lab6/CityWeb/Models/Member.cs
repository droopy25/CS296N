using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CityWeb.Models
{
    public class Member : IdentityUser
    {
        //public int MemberID { get; set; }
       
        public string From { get; set; }
        //public string Email { get; set; }
        
            
    }
}