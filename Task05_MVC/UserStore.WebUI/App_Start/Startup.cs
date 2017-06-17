using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using UserStore.BLL.Interfaces;
using UserStore.BLL.Services;

[assembly: OwinStartup(typeof(UserStore.WebUI.Startup))]
namespace UserStore.WebUI
{
    public class Startup
    {
        readonly IServiceCreator _serviceCreator = new ServiceCreator();

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(CreateUserService);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            return _serviceCreator.CreateUserService("DefaultConnection");
        }
    }
}