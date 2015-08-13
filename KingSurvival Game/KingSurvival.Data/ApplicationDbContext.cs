namespace KingSurvival.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using KingSurvival.Models;
    using KingSurvival.Data.Migrations;

    public class KingSurvivalDbContext : IdentityDbContext<KingSurvivalUser>
    {
        public KingSurvivalDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<KingSurvivalDbContext, Configuration>());
        }

        public IDbSet<Game> Games { get; set; }

        public static KingSurvivalDbContext Create()
        {
            return new KingSurvivalDbContext();
        }
    }
}
