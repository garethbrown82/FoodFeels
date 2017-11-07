namespace FoodFeels.Migrations
{
    using Models;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FoodFeels.Models.FoodFeelsDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FoodFeels.Models.FoodFeelsDBContext context)
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

            context.Foods.AddOrUpdate(
                f => f.FoodName,
                new Food
                {
                    FoodName = "Cheese",
                    Time = DateTime.Parse("2017-09-01 08:30")
                }
                );

            context.Feelings.AddOrUpdate(
                f => f.Time,
                new Feeling
                {
                    FeelLevel = 3,
                    Time = DateTime.Parse("2017-09-01 08:31")
                }
                );
        }
    }
}
