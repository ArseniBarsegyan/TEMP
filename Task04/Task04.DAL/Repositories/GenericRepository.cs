using System;
using System.Data.Entity;
using System.Linq;
using Task04.DAL.Entities;

namespace Task04.DAL.Repositories
{
    public class GenericRepository<TEntity> : IDisposable
        where TEntity : Entity
    {
        private readonly DbContext _dbContext;
        private bool _disposed = false;

        protected IDbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> Get()
        {
            return DbSet;
        }

        public TEntity Get(Guid id)
        {
            return DbSet.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(TEntity item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var item = DbSet.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                DbSet.Remove(item);
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
