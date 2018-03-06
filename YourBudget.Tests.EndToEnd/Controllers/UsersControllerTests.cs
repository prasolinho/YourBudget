using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;
using YourBudget.Api;
using YourBudget.Infrastructure.DTO;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using YourBudget.Infrastructure.Command.Users;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace YourBudget.Tests.EndToEnd.Controllers
{
    public class UsersControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public UsersControllerTests()
        {
            // Arrange

            // Get configuration.
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(@"../../../../YourBudget.API/"))
                .AddJsonFile("appsettings.json", optional: false)
                .AddUserSecrets<Startup>()
                .Build();

            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(configuration));
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task given_valid_email_user_should_exists()
        {
            // Arrange
            string email = "user1@email.com";

            // Act
            var user = await GetUserAsync(email);

            // Assert
            Assert.Equal(email, user.Email);
        }

        [Fact]
        public async Task given_invalid_email_user_should_not_exists()
        {
            // Arrange
            string email = "user1000@email.com";

            // Act
            var response = await _client.GetAsync($"users/{email}");
            
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task given_unique_email_user_should_be_created()
        {
            // Arrange
            string email = "test@email.com";
            var request = new CreateUser
            {
                Email = email,
                UserName = "test",
                Password = "secret"
            };

            // Act
            var response = await _client.PostAsync("users", GetPayload(request));
            
            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal($"users/{request.Email}", response.Headers.Location.ToString());

            var user = await GetUserAsync(email);
            Assert.Equal(request.Email, user.Email);
        }

        private async Task<UserDto> GetUserAsync(string email)
        {
            var response = await _client.GetAsync($"users/{email}");
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserDto>(responseString);
        }

        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}