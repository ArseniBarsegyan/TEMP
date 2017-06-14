using System;
using System.Threading.Tasks;
using ManagerSystem.UserStore.DAL.EF;
using ManagerSystem.UserStore.DAL.Entities;
using ManagerSystem.UserStore.DAL.Identity;
using ManagerSystem.UserStore.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ManagerSystem.UserStore.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private bool _disposed;

        public IdentityUnitOfWork(string connectionString)
        {
            _context = new ApplicationContext(connectionString);
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
            RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_context));
            ClientManager = new ClientManager(_context);
        }

        public ApplicationUserManager UserManager { get; }
        public IClientManager ClientManager { get; }
        public ApplicationRoleManager RoleManager { get; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                UserManager.Dispose();
                RoleManager.Dispose();
                ClientManager.Dispose();
            }
            _disposed = true;
        }
    }
}