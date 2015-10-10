namespace KingSurvival.Data
{
    using KingSurvival.Data.Repositories;
    using KingSurvival.Models;

    public interface IKingSurvivalData
    {
        IRepository<KingSurvivalUser> Users { get; }

        IRepository<Game> Game { get; }

        int SaveChanges();
    }
}