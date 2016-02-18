using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DiplomaDataModel;

namespace OptionsWebSite.DataContext
{
    public class DiplomaOptionsContext : DbContext
    {
        public DiplomaOptionsContext() : base("DefaultConnection") { }

        public DbSet<Choice> Choices { get; set; }
        public DbSet<YearTerm> YearTerms { get; set;}
        public DbSet<Option> Options { get; set; }
    }
}