using Microsoft.AspNet.Identity.EntityFramework;

namespace ManagerSystem.UserStore.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}