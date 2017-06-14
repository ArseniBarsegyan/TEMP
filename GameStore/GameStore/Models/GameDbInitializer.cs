using System.Data.Entity;

namespace GameStore.Models
{
    public class GameDbInitializer : DropCreateDatabaseIfModelChanges<GameContext>
    {
        protected override void Seed(GameContext db)
        {
            base.Seed(db);
        }
    }
}