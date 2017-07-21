using Microsoft.AspNet.Identity;
using WebApplication_JQuery_Bootstrap.DAL.Entities;

namespace WebApplication_JQuery_Bootstrap.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }
    }
}