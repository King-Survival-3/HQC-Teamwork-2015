using KingSurvival.Data.Repositories;
using KingSurvival.Models;

namespace KingSurvival.Data
{
    public interface IKingSurvivalData
    {
        IRepository<KingSurvivalUser> Users { get; }

        IRepository<Game> Game { get; }

        int SaveChanges();
    }
}