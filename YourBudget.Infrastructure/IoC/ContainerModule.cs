using Autofac;
using Microsoft.Extensions.Configuration;
using YourBudget.Infrastructure.IoC.Modules;
using YourBudget.Infrastructure.Mappers;

namespace YourBudget.Infrastructure.IoC
{
    public class ContainerModule : Autofac.Module
    {
        private readonly IConfiguration configuration;
        public ContainerModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.Initialize()).SingleInstance();
            builder.RegisterModule<CommandModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule(new SettingsModule(configuration));
        }
    }
}