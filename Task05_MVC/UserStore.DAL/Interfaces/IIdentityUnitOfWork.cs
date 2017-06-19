using System;
using System.Threading.Tasks;
using UserStore.DAL.Identity;

namespace UserStore.DAL.Interfaces
{
    public interface IIdentityUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}