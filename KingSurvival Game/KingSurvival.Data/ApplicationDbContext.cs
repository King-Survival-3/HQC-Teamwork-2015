namespace KingSurvival.Data
{
    using System.Data.Entity;
    using KingSurvival.Data.Migrations;
    using KingSurvival.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    
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