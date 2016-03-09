namespace Final.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Final.Models.FinalContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Final.Models.FinalContext context)
        {

            UserManager<Member> userManager = new UserManager<Member>(
              new UserStore<Member>(context));

            Member GreenMile = context.Users.Where(u => u.UserName == "ptony25@hotmail.com").FirstOrDefault();
            if (GreenMile == null)
            {
                GreenMile = new Member()
                {
                    UserName = "ptony25@hotmail.com",
                    Name = "Green Mile",
                    Category = "Grower",
                    Phone = "541-345-1234"
                };
                var result = userManager.Create(GreenMile, "password");
                context.SaveChanges();
            };
            Member JJ = context.Users.Where(u => u.UserName == "Admin@JamacialJoels.com").FirstOrDefault();
            if (JJ == null)
            {
                JJ = new Member()
                {
                    UserName = "Admin@JamaciaJoels.com",
                    Name = "Jamacia Joel's",
                    Category = "Dispensary",
                    Phone = "541-123-3457"
                };
                var result = userManager.Create(JJ, "password");
                context.SaveChanges();
            };
            Member G123 = context.Users.Where(u => u.UserName == "Admin@Grower123.com").FirstOrDefault();
            if (G123 == null)
            {
                G123 = new Member()
                {
                    UserName = "Admin@Grower123.com",
                    Name = "Grower123",
                    Category = "Grower",
                    Phone = "654-234-0987"
                };
                var result = userManager.Create(G123, "password");
                context.SaveChanges();
            }
            Member WC = context.Users.Where(u => u.UserName == "Admin@WellnessCenter.com").FirstOrDefault();
            if (WC == null)
            {
                WC = new Member()
                {
                    UserName = "Admin@WellnessCenter.com",
                    Name = "Wellness Center",
                    Category = "Dispensary",
                    Phone = "654-123-7865",
                };
                var result = userManager.Create(WC, "password");
                context.SaveChanges();
            }
            City city1 = new City { CityName = "Springfield" };
            City city2 = new City { CityName = "Eugene" };
            City city3 = new City { CityName = "Seattle" };
            State state1 = new State { StateName = "WA" };
            State state2 = new State { StateName = "OR" };                     
            
            Product flower1 = new Product
            {
                Strain = "Golden Pineapple",
                Type = "Flower",
                Quantity = 3.5m,
                Q_Type = "Pounds"
            };
            Product flower2 = new Product
            {
                Strain = "Gorilla Glue 4",
                Type = "Flower",
                Quantity = 2,
                Q_Type = "Pounds"
            };
            Product clone1 = new Product
            {
                Strain = "ATF",
                Type = "Clone",
                Quantity = 51,
                Q_Type = "EA"
            };
            Product clone2 = new Product
            {
                Strain = "Jack Herer",
                Type = "Clone",
                Quantity = 36,
                Q_Type = "EA"
            };
            Product clone3 = new Product
            {
                Strain = "Lambsbreath Sour Diesel",
                Type = "Clone",
                Quantity = 12,
                Q_Type = "EA"
            };
            Product clone4 = new Product
            {
                Strain = "Girl Scout Cookie",
                Type = "Clone",
                Quantity = 18,
                Q_Type = "EA"
            };

            state1.Cities.Add(city3);
            state2.Cities.Add(city1);
            state2.Cities.Add(city2);
            city1.Members.Add(GreenMile);
            city2.Members.Add(JJ);
            city3.Members.Add(G123);
            city3.Members.Add(WC);
            GreenMile.Products.Add(clone1);
            GreenMile.Products.Add(clone2);
            GreenMile.Products.Add(clone3);
            G123.Products.Add(clone4);
            G123.Products.Add(flower1);
            G123.Products.Add(flower2);
            context.States.AddOrUpdate(s => s.StateName, state1, state2);
            //context.States.Add(state2);

            
            Member M = context.Users.Where(u => u.UserName == "ptony25@gmail.com").FirstOrDefault();
            if (M == null)
            {
                M = new Member() { UserName = "ptony25@gmail.com" };
                var result = userManager.Create(M, "password");
                context.SaveChanges();
            }

            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Admin" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Dispensary" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Grower" });
            context.SaveChanges();

            userManager.AddToRole(M.Id, "Admin");
            userManager.AddToRole(GreenMile.Id, "Grower");
            userManager.AddToRole(JJ.Id, "Dispensary");
            userManager.AddToRole(G123.Id, "Grower");
            userManager.AddToRole(WC.Id, "Dispensary");
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
