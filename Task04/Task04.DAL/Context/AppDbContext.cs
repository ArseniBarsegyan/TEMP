using System.Data.Entity;
using Task04.DAL.Entities;

namespace Task04.DAL.Context
{
    public class AppDbContext : DbContext
    {
        static AppDbContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppDbContext>());
        }

        public AppDbContext(string connectionString) 
            : base (connectionString)
        {
            
        }
        
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<File> Files { get; set; }
    }
}
