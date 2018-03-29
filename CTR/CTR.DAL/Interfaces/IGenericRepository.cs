using System;
using System.Linq;
using System.Threading.Tasks;
using CTR.DAL.Entities;

namespace CTR.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : Entity
    {
        IQueryable<TEntity> GetAll();
        Task CreateAsync(TEntity item);
        Task<TEntity> GetByIdAsync(int? id);
        void Update(TEntity item);
        Task DeleteAsync(int? id);
        Task SaveAsync();
    }
}