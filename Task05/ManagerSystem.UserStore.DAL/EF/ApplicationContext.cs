using System.Data.Entity;
using ManagerSystem.UserStore.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ManagerSystem.UserStore.DAL.EF
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string connectionString)
            : base(connectionString)
        {

        }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}