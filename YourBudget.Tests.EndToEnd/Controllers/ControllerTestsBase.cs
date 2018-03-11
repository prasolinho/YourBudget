using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
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
            // Konfiguracja taka potrzeban, ponieważ w Startupie dodaliśmy obsługę JWT Tokens
            // i potrzebne są dane z pliku appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(@"../../../../YourBudget.API/"))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            Server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
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
