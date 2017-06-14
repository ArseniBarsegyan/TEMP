using System;
using System.Threading.Tasks;
using ManagerSystem.UserStore.DAL.Identity;

namespace ManagerSystem.UserStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}