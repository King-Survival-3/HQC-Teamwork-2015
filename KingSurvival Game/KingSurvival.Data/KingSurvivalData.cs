namespace KingSurvival.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using KingSurvival.Data.Repositories;
    using KingSurvival.Models;

    public class KingSurvivalData : IKingSurvivalData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public KingSurvivalData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<KingSurvivalUser> Users
        {
            get
            {
                return this.GetRepository<KingSurvivalUser>();
            }
        }

        public IRepository<Game> Game
        {
            get
            {
                return this.GetRepository<Game>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), this.context);
                this.repositories[typeOfRepository] = newRepository;
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}