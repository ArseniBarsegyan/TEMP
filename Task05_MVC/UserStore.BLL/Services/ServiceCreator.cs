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

        public IOrderService CreateOrderService(string connectionString)
        {
            return new OrderService(new UnitOfWork(connectionString));
        }

        public IManagerService CreateManagerService(string connectionString)
        {
            return new ManagerService(new UnitOfWork(connectionString));
        }

        public IProductService CreateProductService(string connectionString)
        {
            return new ProductService(new UnitOfWork(connectionString));
        }
    }
}