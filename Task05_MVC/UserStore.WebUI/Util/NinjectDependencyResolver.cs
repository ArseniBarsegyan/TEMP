using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using UserStore.BLL.Interfaces;
using UserStore.BLL.Services;
using UserStore.DAL.Interfaces;
using UserStore.DAL.Repositories;

namespace UserStore.WebUI.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("DefaultConnection");

            kernel.Bind<IUnitOfWork>()
                .To<UnitOfWork>()
                .WhenInjectedInto<OrderService>()
                .WithConstructorArgument("DefaultConnection");

            kernel.Bind<IUnitOfWork>()
                .To<UnitOfWork>()
                .WhenInjectedInto<ProductService>()
                .WithConstructorArgument("DefaultConnection");

            kernel.Bind<IUnitOfWork>()
                .To<UnitOfWork>()
                .WhenInjectedInto<UserService>()
                .WithConstructorArgument("DefaultConnection");

            kernel.Bind<IUnitOfWork>()
                .To<UnitOfWork>()
                .WhenInjectedInto<ManagerService>()
                .WithConstructorArgument("DefaultConnection");

            kernel.Bind<IOrderService>().To<OrderService>();
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IManagerService>().To<ManagerService>();
        }
    }
}