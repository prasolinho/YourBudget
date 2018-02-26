using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Xunit;
using YourBudget.Core.Domain;
using YourBudget.Core.Repositories;
using YourBudget.Infrastructure.Services;

namespace YourBudget.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_async_should_invoke_add_async_on_repository()
        {
            var userRepository = new Mock<IUserRepository>();
            var mapper = new Mock<IMapper>();

            var userService = new UserService(userRepository.Object, mapper.Object);
            await userService.RegisterAsync("user12@email.com", "user12", "secret12");

            userRepository.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}