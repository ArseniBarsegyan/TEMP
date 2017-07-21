using System;
using System.Threading.Tasks;
using WebApplication_JQuery_Bootstrap.DAL.Entities;
using WebApplication_JQuery_Bootstrap.DAL.Identity;
using WebApplication_JQuery_Bootstrap.DAL.Repositories;

namespace WebApplication_JQuery_Bootstrap.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        GenericRepository<Game> GameRepository { get; }
        Task SaveAsync();
    }
}