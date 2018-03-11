using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using YourBudget.Infrastructure.Command.Users;
using YourBudget.Infrastructure.DTO;

namespace YourBudget.Tests.EndToEnd.Controllers.UsersController
{
    public class RegisterUserTests : ControllerTestsBase
    {
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
            var response = await Client.PostAsync("users", GetPayload(request));
            
            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal($"users/{request.Email}", response.Headers.Location.ToString());

            var user = await GetUserAsync(email);
            Assert.Equal(request.Email, user.Email);
        }

        private async Task<UserDto> GetUserAsync(string email)
        {
            var response = await Client.GetAsync($"users/{email}");
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserDto>(responseString);
        }
    }
}