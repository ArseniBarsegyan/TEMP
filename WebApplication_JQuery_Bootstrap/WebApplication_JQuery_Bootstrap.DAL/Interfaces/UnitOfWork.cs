using System;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using WebApplication_JQuery_Bootstrap.DAL.EF;
using WebApplication_JQuery_Bootstrap.DAL.Entities;
using WebApplication_JQuery_Bootstrap.DAL.Identity;
using WebApplication_JQuery_Bootstrap.DAL.Repositories;

namespace WebApplication_JQuery_Bootstrap.DAL.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _context;
        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _context = new ApplicationContext(connectionString);
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
            RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_context));
            GameRepository = new GenericRepository<Game>(_context);
        }

        public ApplicationUserManager UserManager { get; }
        public ApplicationRoleManager RoleManager { get; }
        public GenericRepository<Game> GameRepository { get; }

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
            }
            _disposed = true;
        }
    }
}