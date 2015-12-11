namespace MoTMe.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using MoTMe.Models;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MoTMe.Models.MoTMeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MoTMe.Models.MoTMeContext context)
        {
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
            Message firstMessage = new Message { Id = 1, AuthorId = 1, RecieverId = 2, Body = "This is the first message in the database", Date = DateTime.Now };
            context.Messages.Add(firstMessage);
            context.SaveChanges();
            //context.Users.Add();
        }
    }
}
