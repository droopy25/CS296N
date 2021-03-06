﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;

namespace CityWeb.Models
{
    public class CityWebContext : IdentityDbContext<Member>
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public CityWebContext() : base("name=CityWebContext")
        {
        }

        public System.Data.Entity.DbSet<CityWeb.Models.Forum> Fora { get; set; }

        public System.Data.Entity.DbSet<CityWeb.Models.Message> Messages { get; set; }

        public System.Data.Entity.DbSet<CityWeb.Models.Topic> Topics { get; set; }

      
    }
}
