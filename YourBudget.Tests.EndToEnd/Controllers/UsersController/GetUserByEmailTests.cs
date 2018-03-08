using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;
using YourBudget.Api;
using YourBudget.Infrastructure.DTO;
using FluentAssertions;

namespace YourBudget.Tests.EndToEnd.Controllers.UsersController
{
    public class GetUserByEmailTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        private string email;

        public GetUserByEmailTests()
        {
            // Arrange

            // Konfiguracja taka potrzeban, ponieważ w Startupie dodaliśmy obsługę JWT Tokens
            // i potrzebne są dane z pliku appsettings.json
            
            // Get configuration.
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(@"../../../../YourBudget.API/"))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(configuration));
            _client = _server.CreateClient();

            email = null;
        }

        [Fact]
        public async Task given_valid_email_user_should_exists()
        {
            // Arrange
            email = "user1@email.com";

            // Act
            var user = await GetUserAsync(email);

            // Assert
            email.Should().BeEquivalentTo(user.Email);
        }
        
        [Fact]
        public async Task given_invalid_email_user_should_not_exists()
        {
            // Arrange
            email = "user1000@email.com";

            // Act
            var response = await Execute();
            
            // Assert
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);
        }

        private async Task<UserDto> GetUserAsync(string email)
        {
            var response = await Execute();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserDto>(responseString);
        }

        private async Task<HttpResponseMessage> Execute()
        {
            return await _client.GetAsync($"users/{email}");
        }
    }
}