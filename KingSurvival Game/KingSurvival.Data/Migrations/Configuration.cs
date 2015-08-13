namespace KingSurvival.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KingSurvivalDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
           //this.ContextKey = "KingSurvival.Data.KingSurvivalDbContext";
            this.AutomaticMigrationDataLossAllowed = true;
             
        }
    }
}
