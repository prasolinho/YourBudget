using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit;
using YourBudget.Api;
using YourBudget.Infrastructure.Command.Users;
using YourBudget.Infrastructure.DTO;

namespace YourBudget.Tests.EndToEnd.Controllers.UsersController
{
    public class RegisterUserTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public RegisterUserTests()
        {
            // Arrange

            // Get configuration.
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(@"../../../../YourBudget.API/"))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(configuration));
            _client = _server.CreateClient();
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