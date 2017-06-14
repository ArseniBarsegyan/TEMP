namespace ManagerSystem.UserStore.BLL.Interfaces
{
    //can be replaced by Ninject DI
    public interface IServiceCreator
    {
        IUserService CreateUserService(string connection);
    }
}