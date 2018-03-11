using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;
using YourBudget.Core.Domain;
using YourBudget.Core.Repositories;
using YourBudget.Infrastructure.Services;

namespace YourBudget.Tests.Services.UserService
{
    public class LogInTests
    {
        private Mock<IUserRepository> userRepository;
        private readonly Mock<IEncrypter> encrypter;
        private readonly Mock<IMapper> mapper;

        private string email;
        private string password;
        private string hash;

        public LogInTests()
        {
            userRepository = new Mock<IUserRepository>();
            encrypter = new Mock<IEncrypter>();
            mapper = new Mock<IMapper>();

            email = "test@test.pl";
            password = "mySafePassword";
        }

        [Fact]
        public async void invalid_email_user_doesnt_exists_throw_exception()
        {
            // Arrange
            userRepository.Setup(x => x.GetAsync(email)).ReturnsAsync((User)null);

            // Act
            Exception exc = null;
            try
            {
                await Execute();
            }
            catch (Exception ex)
            {
                exc = ex;
            }
            
            // Assert
            exc.Should().NotBeNull();
            exc.Message.Should().BeEquivalentTo("Invalid credentials");
        }   

        [Fact]
        public async void incorrect_password_throw_exception()
        {
            // Arrange
            hash = "qwertyuiopasdfghjklzxcvbnm";

            User user = new User(email, "TP", hash, "salt", "user");
            userRepository.Setup(x => x.GetAsync(email)).ReturnsAsync(user);
            encrypter.Setup(x => x.GetHash(password, user.Salt)).Returns(hash + "XXX");

            // Act
            Exception exc = null;
            try
            {
                await Execute();
            }
            catch (Exception ex)
            {
                exc = ex;
            }
            
            // Assert
            exc.Should().NotBeNull();
            exc.Message.Should().BeEquivalentTo("Invalid credentials");
            encrypter.Verify(x => x.GetHash(password, user.Salt));
        }   

        [Fact]
        public async void correct_credential_login_user()
        {
            // Arrange
            hash = "qwertyuiopasdfghjklzxcvbnm";

            User user = new User(email, "TP", hash, "salt", "user");
            userRepository.Setup(x => x.GetAsync(email)).ReturnsAsync(user);
            encrypter.Setup(x => x.GetHash(password, user.Salt)).Returns(hash);

            // Act
            var result = await Execute();
            
            // Assert
            result.Should().BeTrue();
        }

        private async Task<bool> Execute()
        {
            var userService = new Infrastructure.Services.UserService(userRepository.Object, encrypter.Object, mapper.Object);
            return await userService.LoginAsync(email, password);
        }
    }
}