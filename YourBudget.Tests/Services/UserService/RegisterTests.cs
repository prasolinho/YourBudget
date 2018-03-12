using System;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Xunit;
using YourBudget.Core.Domain;
using YourBudget.Core.Repositories;
using YourBudget.Infrastructure.Services;

namespace YourBudget.Tests.Services.UserService
{
    public class RegisterTests
    {
        private readonly Mock<IUserRepository> userRepository;
        private readonly Mock<IMapper> mapper;

        private readonly Mock<IEncrypter> encrypter;

        private string email = "user12@email.com";
        private Guid userId = Guid.NewGuid();

        public RegisterTests()
        {
            userRepository = new Mock<IUserRepository>();
            mapper = new Mock<IMapper>();
            encrypter = new Mock<IEncrypter>();
        }

        [Fact]
        public async Task register_async_should_invoke_add_async_on_repository()
        {
            // Arrange

            // TODO: Czy tak może zostać? Popawić?
            encrypter.Setup(e => e.GetSalt()).Returns("aaaaaa");
            encrypter.Setup(e => e.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("bbbbbb");

            // Act
            await Execute();

            // Assert
            userRepository.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async void register_async_throw_exception_when_user_already_exists()
        {
            // Arrange
            userRepository.Setup(ga => ga.GetAsync(email)).ReturnsAsync(new User(userId, email, "user12", "secret12", Guid.NewGuid().ToString("N"), "user"));

            Exception exc = null;
            try
            {
                // Act
                await Execute();
            }
            catch (Exception e)
            {
                exc = e;
            }
            
            // Assert
            Assert.NotNull(exc);
        }

        private async Task Execute()
        {
            var userService = new Infrastructure.Services.UserService(userRepository.Object, encrypter.Object, mapper.Object);
            await userService.RegisterAsync(userId, email, "user12", "secret12", "user");
        }
    }
}