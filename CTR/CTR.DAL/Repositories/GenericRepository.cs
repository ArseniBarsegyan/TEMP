using System;
using System.Linq;
using System.Threading.Tasks;
using CTR.DAL.Entities;
using CTR.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CTR.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : Entity
    {
        private readonly DbContext _dbContext;
        private bool _disposed;

        protected DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public async Task CreateAsync(TEntity item)
        {
            await DbSet.AddAsync(item);
        }

        public async Task<TEntity> GetByIdAsync(int? id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(TEntity item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int? id)
        {
            var item = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                DbSet.Remove(item);
            }
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
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