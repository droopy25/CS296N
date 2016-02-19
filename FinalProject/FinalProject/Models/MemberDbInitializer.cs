using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    //public class MemberDbInitializer : DropCreateDatabaseIfModelChanges<FinalProjectContext>
    public class MemberDbInitializer : DropCreateDatabaseAlways<FinalProjectContext>
    {
        protected override void Seed(FinalProjectContext context)
        {

            Category category1 = new Category { CatType = "Dispensary" };
            Category category2 = new Category { CatType = "Grower" };
            City city1 = new City { CityName = "Springfield" };
            City city2 = new City { CityName = "Eugene" };
            City city3 = new City { CityName = "Seattle" };
            State state1 = new State { StateName = "WA" };
            State state2 = new State { StateName = "OR" };
            Member member1 = new Member
            {
                Name = "Green Mile",
                Street = "1234 Main st",
                State = "OR",
                City = "Springfield",
                Phone = "541-345-1234",
                Email = "Admin@GreenMile.com"
            };
            Member member2 = new Member
            {
                Name = "Jamacia Joel's",
                Street = "13th st",
                City = "Eugene",
                State = "OR",
                Phone = "541-123-3457",
                Email = "Admin@JamaciaJoel.com"
            };
            Member member3 = new Member
            {
                Name = "Grower",
                Street = "Main",
                City = "Seattle",
                State = "WA",
                Phone = "654-234-0987",
                Email = "Grower@grower.com"
            };
            Member member4 = new Member
            {
                Name = "Wellness Center",
                Street = "56th st",
                City = "Seattle",
                State = "WA",
                Phone = "654-123-7865",
                Email = "Admin@wellnesscenter.com"
            };
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
            
            
            category1.Members.Add(member2);
            category1.Members.Add(member4);
            
            member1.Products.Add(clone1);
            member1.Products.Add(clone2);
            member1.Products.Add(clone3);
            member3.Products.Add(clone4);
            member3.Products.Add(flower1);
            member3.Products.Add(flower2);
            category2.Members.Add(member1);
            category2.Members.Add(member3);
            context.Categories.Add(category1);
            context.Categories.Add(category2);

            base.Seed(context);
        }
    }
}