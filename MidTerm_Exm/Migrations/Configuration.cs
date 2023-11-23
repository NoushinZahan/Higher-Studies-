namespace MidTerm_Exm.Migrations
{
    using MidTerm_Exm.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MidTerm_Exm.Models.CountryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MidTerm_Exm.Models.CountryDbContext context)
        {
            
        }
    }
}
