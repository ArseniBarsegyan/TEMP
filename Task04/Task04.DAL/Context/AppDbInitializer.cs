using System.Data.Entity;

namespace Task04.DAL.Context
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            base.Seed(context);
        }
    }
}
