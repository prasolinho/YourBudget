using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog.Web;
using System.Net.Http;
using System.Text;
using YourBudget.Api;

namespace YourBudget.Tests.EndToEnd.Controllers
{
    public abstract class ControllerTestsBase
    {
        protected readonly TestServer Server;
        protected readonly HttpClient Client;

        protected ControllerTestsBase()
        {
            // https://github.com/aspnet/Hosting/issues/1191

            // Konfiguracja taka potrzeban, ponieważ w Startupie dodaliśmy obsługę JWT Tokens
            // i potrzebne są dane z pliku appsettings.json
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            Server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog()  // NLog: setup NLog for Dependency injection
                .UseConfiguration(configuration));
            Client = Server.CreateClient();
        }

        protected static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
