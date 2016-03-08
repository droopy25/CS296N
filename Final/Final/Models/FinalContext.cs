using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;

namespace Final.Models
{
    public class FinalContext : IdentityDbContext<Member>
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public FinalContext() : base("name=FinalContext")
        {
        }

        public DbSet<Final.Models.Product> Products { get; set; }

        

        public DbSet<Final.Models.City> Cities { get; set; }

        public DbSet<Final.Models.State> States { get; set; }

        //public System.Data.Entity.DbSet<Final.Models.Member> Members { get; set; }
        /*public override int SaveChanges()
        {
            try
            {
                var x = base.SaveChanges();
                return x;
            }
            catch(DbEntityValidationException ex)
            {
                var sb = new StringBuilder();
                foreach (var f in ex.EntityValidationErrors)
                    foreach (var e in f.ValidationErrors)
                        sb.AppendFormat("{0} : {1}\n", e.PropertyName, e.ErrorMessage);
                throw new DbEntityValidationException(sb.ToString(), ex);
            }
        }*/
    }
}