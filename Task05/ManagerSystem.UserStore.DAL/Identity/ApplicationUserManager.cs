using ManagerSystem.UserStore.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace ManagerSystem.UserStore.DAL.Identity
{
    //Controlling users : add them to DB and auth them
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
            
        }
    }
}