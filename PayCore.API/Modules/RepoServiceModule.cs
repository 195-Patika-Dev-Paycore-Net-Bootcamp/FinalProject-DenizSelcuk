using Autofac;
using Paycore.Repository;
using Paycore.Repository.Repositories;
using PayCore.Core.Repositories;
using PayCore.Core.Services;
using PayCore.Core.UnitOfWork;
using PayCore.Service.Mapping;
using PayCore.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace PayCore.API.Modules
{
    public class RepoServiceModule : Module
    {
        //AutoFac kütüphanesi ile serviceleri register ettik.
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericService<>)).As(typeof(IGenericService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var apiAssembly = Assembly.GetExecutingAssembly();

            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));

            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            //Sonu "Repository" ile biten classları register eder
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            //Sonu "Service" ile biten classları register eder
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
