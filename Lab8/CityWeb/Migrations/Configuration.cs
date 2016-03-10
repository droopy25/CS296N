namespace CityWeb.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CityWeb.Models.CityWebContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "CityWeb.Models.CityWebContext";
        }

        protected override void Seed(CityWeb.Models.CityWebContext context)
        {
            Topic topic1 = new Topic { Category = "For Sale" };
            Topic topic2 = new Topic { Category = "Help Wanted" };
            Message message1 = new Message
            {
                Subject = "Chevy Tahoe",
                Body = "I have a 2010 Chevy Tahoe great condition for a great price",
                Date = DateTime.Now.ToString(),
                From = "Tony Plueard"

            };
            Message message2 = new Message
            {
                Subject = "Scion TC",
                Body = "2012 Scion TC for sale",
                Date = DateTime.Today.ToString(),
                From = "Tammi Plueard"
            };
            Message message3 = new Message
            {
                Subject = "Clerk Needed",
                Body = "Fast paced corner market in need of clerk",
                Date = DateTime.Now.ToString(),
                From = "Tony Plueard"

            };
            Message message4 = new Message
            {
                Subject = "Programmer Needed",
                Body = "Programmer needed at local IT company",
                Date = DateTime.Now.ToString(),
                From = "Tammi Plueard"

            };
            topic1.Messages.Add(message1);
            topic1.Messages.Add(message2);
            topic2.Messages.Add(message3);
            topic2.Messages.Add(message4);
            context.Topics.AddOrUpdate(t => t.TopicID, topic1, topic2);
            //context.Topics.Add(topic2);

            UserManager<Member> userManager = new UserManager<Member>(
              new UserStore<Member>(context));
            Member M = context.Users.Where(u => u.UserName == "ptony25@gmail.com").FirstOrDefault();
            if(M == null)
            {
                M = new Member() { UserName = "ptony25@gmail.com", From = "Tony" };
                var result = userManager.Create(M, "password");
                context.SaveChanges();
            }
                           
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Admin" });
            context.SaveChanges();

            userManager.AddToRole(M.Id, "Admin");
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
