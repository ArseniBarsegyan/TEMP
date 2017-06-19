﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using UserStore.DAL.EF;
using UserStore.DAL.Entities;
using UserStore.DAL.Identity;
using UserStore.DAL.Interfaces;

namespace UserStore.DAL.Repositories
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        private ApplicationContext _context;
        private bool _disposed;

        public IdentityUnitOfWork(string connectionString)
        {
            _context = new ApplicationContext(connectionString);
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
            RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_context));
            ClientRepository = new GenericRepository<Client>(_context);
            ProductRepository = new GenericRepository<Product>(_context);
            ManagerRepository = new GenericRepository<Manager>(_context);
        }

        public ApplicationUserManager UserManager { get; }
        public ApplicationRoleManager RoleManager { get; }
        public GenericRepository<Client> ClientRepository { get; }
        public GenericRepository<Product> ProductRepository { get; }
        public GenericRepository<Manager> ManagerRepository { get; }

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