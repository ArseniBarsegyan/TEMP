using UserStore.BLL.Interfaces;
using UserStore.DAL.Repositories;

namespace UserStore.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connectionString)
        {
            return new UserService(new UnitOfWork(connectionString));
        }
    }
}