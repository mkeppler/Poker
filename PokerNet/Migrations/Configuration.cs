namespace PokerNet.Migrations
{
    using PokerNet.Repository.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Repository.RepositoryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Repository.RepositoryContext context)
        {
            if (!context.People.Any())
            {
                context.People.AddOrUpdate(x => x.ID,
                    new Person { ID = Guid.NewGuid(), FirstName = "James", Order = 1 },
                    new Person { ID = Guid.NewGuid(), FirstName = "Jeff", Order = 2 },
                    new Person { ID = Guid.NewGuid(), FirstName = "Kevin", Order = 3 },
                    new Person { ID = Guid.NewGuid(), FirstName = "Christian", Order = 4 },
                    new Person { ID = Guid.NewGuid(), FirstName = "Aaron", Order = 5 },
                    new Person { ID = Guid.NewGuid(), FirstName = "Greg", Order = 6 },
                    new Person { ID = Guid.NewGuid(), FirstName = "Matt", Order = 7 }
                    );
            }

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
