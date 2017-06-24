using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Owin;
using UserStore.BLL.Interfaces;
using UserStore.BLL.Services;
using UserStore.DAL.Interfaces;
using UserStore.DAL.Repositories;

[assembly: OwinStartup(typeof(UserStore.WebUI.Startup))]
namespace UserStore.WebUI
{
    public class Startup
    {
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
            IKernel kernel = new StandardKernel();

            kernel.Bind<IUserService>().To<UserService>();

            kernel.Bind<IUnitOfWork>()
                .To<UnitOfWork>()
                .WhenInjectedInto<UserService>()
                .WithConstructorArgument("DefaultConnection");

            return kernel.TryGet<IUserService>();
        }
    }
}