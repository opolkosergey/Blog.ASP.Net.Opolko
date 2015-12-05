
using DalToWeb;
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
        }
    }
}