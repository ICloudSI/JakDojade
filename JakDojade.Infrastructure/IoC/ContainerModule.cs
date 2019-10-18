using Autofac;
using AutoMapper.Configuration;
using JakDojade.Infrastructure.AutoMapper;
using JakDojade.Infrastructure.IoC.Modules;

namespace JakDojade.Infrastructure.IoC
{
    public class ContainerModule : Autofac.Module
    {
         private readonly IConfiguration _configuration;

        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.Initialize())
                .SingleInstance();

            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule(new SettingsModule(_configuration));
        }          
    }
}