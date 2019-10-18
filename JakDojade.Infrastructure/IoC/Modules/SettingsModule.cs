using Autofac;
using AutoMapper.Configuration;

namespace JakDojade.Infrastructure.IoC.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
 //           builder.RegisterInstance(_configuration.GetSettings<GeneralSettings>())
 //                  .SingleInstance();
		}  
    }
}