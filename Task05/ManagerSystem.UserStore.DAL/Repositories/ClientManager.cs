using ManagerSystem.UserStore.DAL.EF;
using ManagerSystem.UserStore.DAL.Entities;
using ManagerSystem.UserStore.DAL.Interfaces;

namespace ManagerSystem.UserStore.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public ApplicationContext Context { get; }

        public ClientManager(ApplicationContext context)
        {
            Context = context;
        }

        public void Create(ClientProfile item)
        {
            Context.ClientProfiles.Add(item);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}