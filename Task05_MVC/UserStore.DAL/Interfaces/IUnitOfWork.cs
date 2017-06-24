using System;
using System.Threading.Tasks;
using UserStore.DAL.Entities;
using UserStore.DAL.Identity;
using UserStore.DAL.Repositories;

namespace UserStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        GenericRepository<Client> ClientRepository { get; }
        GenericRepository<Product> ProductRepository { get; }
        GenericRepository<Manager> ManagerRepository { get; }
        GenericRepository<Order> OrderRepository { get; }
        Task SaveAsync();
        void Save();
    }
}