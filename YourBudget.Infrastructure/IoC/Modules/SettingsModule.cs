using Autofac;
using Microsoft.Extensions.Configuration;
using YourBudget.Infrastructure.Extensions;
using YourBudget.Infrastructure.Settings;

namespace YourBudget.Infrastructure.IoC.Modules
{
    public class SettingsModule : Autofac.Module
    {
        private readonly IConfiguration configuration;
        public SettingsModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(configuration.GetSettings<GeneralSettings>())
                .SingleInstance();
        }
    }
}