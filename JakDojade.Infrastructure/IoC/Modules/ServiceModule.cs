using System.Reflection;
using Autofac;
using JakDojade.Infrastructure.Services;

namespace JakDojade.Infrastructure.IoC.Modules
{
    public class ServiceModule : Autofac.Module
    {
         protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServiceModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                   .Where(x => x.IsAssignableTo<IService>())
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterType<Encrypter>()
                   .As<IEncrypter>()
                   .SingleInstance();
        }
        
    }
}