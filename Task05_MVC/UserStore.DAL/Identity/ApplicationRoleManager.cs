using Microsoft.AspNet.Identity;
using UserStore.DAL.Entities;

namespace UserStore.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store) 
            : base(store)
        {

        }
    }
}