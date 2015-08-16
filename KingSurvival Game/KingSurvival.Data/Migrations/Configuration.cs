namespace KingSurvival.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<KingSurvivalDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            ////this.ContextKey = "KingSurvival.Data.KingSurvivalDbContext";
            this.AutomaticMigrationDataLossAllowed = true;
        }
    }
}