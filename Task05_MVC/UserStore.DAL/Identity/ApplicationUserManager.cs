using Microsoft.AspNet.Identity;
using UserStore.DAL.Entities;

namespace UserStore.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) 
            : base(store)
        {

        }
    }
}