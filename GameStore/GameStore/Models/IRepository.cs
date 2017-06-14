using System.Collections.Generic;

namespace GameStore.Models
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IEnumerable<TEntity> GetGameList();
        TEntity GetGame(int id);
        void Create(TEntity item);
        void Delete(int id);
        void Correct(TEntity item);
        void Save();
    }
}
