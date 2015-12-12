
using System.Data.Entity;
using Bll.Interface.Services;
using Bll.Services;
using DalToWeb;
using DalToWeb.Concrete;
using DalToWeb.Interfacies;
using DalToWeb.Repositories;
using Ninject.Modules;

namespace CustomAuth.Infrastructure
{
    public class DependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IUserService>().To<UserService>();
            Bind<DbContext>().To<UserContext>();
            Bind<IBlogService>().To<BlogService>();
            Bind<IBlogRepository>().To<BlogRepository>();
        }
    }
}