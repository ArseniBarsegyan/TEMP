using Microsoft.AspNet.Identity;
using WebApplication_JQuery_Bootstrap.DAL.Entities;

namespace WebApplication_JQuery_Bootstrap.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store) 
            : base(store)
        {
        }
    }
}