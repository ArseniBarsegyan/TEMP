using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication_JQuery_Bootstrap.DAL.Entities;

namespace WebApplication_JQuery_Bootstrap.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        static ApplicationContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationContext>());
        }

        public ApplicationContext(string connectionString)
            : base(connectionString)
        {

        }

        public DbSet<Game> Games { get; set; }
    }
}