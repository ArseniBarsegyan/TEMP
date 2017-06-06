using System.Data.Entity;
using TestMapper.DAO;

namespace TestMapper.Context
{
    public class AppContext : DbContext
    {
        public AppContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppContext>());
        }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
