using ManagerSystem.UserStore.BLL.Interfaces;
using ManagerSystem.UserStore.DAL.Repositories;

namespace ManagerSystem.UserStore.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }
    }
}