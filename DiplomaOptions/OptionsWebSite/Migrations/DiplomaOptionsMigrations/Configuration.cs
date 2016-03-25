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

            //seed choices
            context.Choices.AddOrUpdate(
                c => c.ChoiceId, getChoices().ToArray());
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

        private List<Choice> getChoices()
        {
            List<Choice> choices = new List<Choice>()
            {
                new Choice()
                {
                    YearTermId = 2,
                    StudentId = "A00111100",
                    StudentFirstName = "Gub",
                    StudentLastName = "Doe",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 3,
                    StudentId = "A00111144",
                    StudentFirstName = "Bob",
                    StudentLastName = "Doe",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 2,
                    StudentId = "A00111112",
                    StudentFirstName = "Aohn",
                    StudentLastName = "Doer",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 2,
                    StudentId = "A00111113",
                    StudentFirstName = "Pohn",
                    StudentLastName = "Roe",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 2,
                    StudentId = "A00111114",
                    StudentFirstName = "Oohn",
                    StudentLastName = "Snoe",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 2,
                    StudentId = "A00111115",
                    StudentFirstName = "Wohn",
                    StudentLastName = "Joe",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 2,
                    StudentId = "A00111116",
                    StudentFirstName = "Vohn",
                    StudentLastName = "Boe",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 2,
                    StudentId = "A00111117",
                    StudentFirstName = "Rohn",
                    StudentLastName = "Foe",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 2,
                    StudentId = "A00111118",
                    StudentFirstName = "Lohn",
                    StudentLastName = "Soe",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 2,
                    StudentId = "A00111119",
                    StudentFirstName = "Hohn",
                    StudentLastName = "Goe",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 2,
                    StudentId = "A00200000",
                    StudentFirstName = "Sohn",
                    StudentLastName = "Eoe",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                //10 for 201530
                new Choice()
                {
                    YearTermId = 3,
                    StudentId = "A00111112",
                    StudentFirstName = "Aohn",
                    StudentLastName = "Doer",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 3,
                    StudentId = "A00111113",
                    StudentFirstName = "Pohn",
                    StudentLastName = "Roe",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 3,
                    StudentId = "A00111114",
                    StudentFirstName = "Oohn",
                    StudentLastName = "Snoe",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 3,
                    StudentId = "A00111115",
                    StudentFirstName = "Wohn",
                    StudentLastName = "Joe",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 3,
                    StudentId = "A00111116",
                    StudentFirstName = "Vohn",
                    StudentLastName = "Boe",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 3,
                    StudentId = "A00111117",
                    StudentFirstName = "Rohn",
                    StudentLastName = "Foe",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 3,
                    StudentId = "A00111118",
                    StudentFirstName = "Lohn",
                    StudentLastName = "Soe",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 3,
                    StudentId = "A00111119",
                    StudentFirstName = "Hohn",
                    StudentLastName = "Goe",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                },
                new Choice()
                {
                    YearTermId = 3,
                    StudentId = "A00200000",
                    StudentFirstName = "Sohn",
                    StudentLastName = "Eoe",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                }
            };

            return choices;
        }
    }
}
