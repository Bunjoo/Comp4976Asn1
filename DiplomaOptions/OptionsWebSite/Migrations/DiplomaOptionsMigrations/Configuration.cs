namespace OptionsWebSite.Migrations.DiplomaOptionsMigrations
{
    using DiplomaDataModel;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OptionsWebSite.DataContext.DiplomaOptionsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\DiplomaOptionsMigrations";
        }

        protected override void Seed(OptionsWebSite.DataContext.DiplomaOptionsContext context)
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

            context.YearTerms.AddOrUpdate(
                y => y.YearTermId, getYearTerm().ToArray());
            context.SaveChanges();

            context.Options.AddOrUpdate(
                o => o.OptionId, getOptions().ToArray());
            context.SaveChanges();
        }

        private List<YearTerm> getYearTerm()
        {
            List<YearTerm> yearTerms = new List<YearTerm>()
            {
                new YearTerm()
                {
                    Year = 2015,
                    Term = 20,
                    isDefault = false
                },
                new YearTerm()
                {
                    Year = 2015,
                    Term = 30,
                    isDefault = false
                },
                new YearTerm()
                {
                    Year = 2016,
                    Term = 10,
                    isDefault = false
                },
                new YearTerm()
                {
                    Year = 2016,
                    Term = 30,
                    isDefault = true
                },
            };

            return yearTerms;
        }

        private List<Option> getOptions()
        {
            List<Option> options = new List<Option>
            {
                new Option
                {
                    Title = "Data Communications",
                    isActive = true
                },
                new Option
                {
                    Title = "Client Server",
                    isActive = true
                },
                new Option
                {
                    Title = "Digital Processing",
                    isActive = true
                },
                new Option
                {
                    Title = "Information Systems",
                    isActive = true
                },
                new Option
                {
                    Title = "Database",
                    isActive = false
                },
                new Option
                {
                    Title = "Web & Mobile",
                    isActive = true
                },
                new Option
                {
                    Title = "Tech Pro",
                    isActive = false
                },
            };

            return options;
        }
    }
}
