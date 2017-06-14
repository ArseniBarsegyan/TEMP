using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GameStore.Models
{
    public class GameRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        private DbContext db;

        public GameRepository(DbContext db)
        {
            this.db = db;
        }

        protected IDbSet<TEntity> dbSet
        {
            get
            {
                return db.Set<TEntity>();
            }
        }

        public IEnumerable<TEntity> GetGameList()
        {
            return dbSet.ToList();
        }

        public TEntity GetGame(int id)
        {
            return dbSet.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Create(TEntity item)
        {
            if (item != null)
            {
                dbSet.Add(item);
            }
        }

        public void Correct(TEntity item)
        {
            if (item != null)
            {
                db.Entry(item).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            TEntity item = dbSet.Where(x => x.Id == id).FirstOrDefault();
            if (item != null)
            {
                dbSet.Remove(item);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}