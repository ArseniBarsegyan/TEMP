using System;

namespace GameStore.Models
{
    public class UnitOfWork : GameContext
    {
        private GameContext db = new GameContext();
        private GameRepository<Game> gameRepository;
        private bool disposed = false;

        public GameRepository<Game> Games
        {
            get
            {
                if (gameRepository == null)
                    gameRepository = new GameRepository<Game>(db);
                return gameRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }        

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}