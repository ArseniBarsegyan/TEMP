using System.Data.Entity;

namespace GameStore.Models
{
    public class GameContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
    }
}