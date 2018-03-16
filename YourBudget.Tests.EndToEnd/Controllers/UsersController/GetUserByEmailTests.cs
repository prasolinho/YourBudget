using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using YourBudget.Infrastructure.DTO;
using FluentAssertions;

namespace YourBudget.Tests.EndToEnd.Controllers.UsersController
{
    public class GetUserByEmailTests : ControllerTestsBase
    {
        private string email;

        public GetUserByEmailTests()
        {
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
            return await Client.GetAsync($"users/{email}");
        }
    }
}