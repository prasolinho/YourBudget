using System;
using FluentAssertions;
using Xunit;

namespace YourBudget.Tests.Services.Encrypter
{
    public class GetHashTests
    {
        private string password;
        private string salt;

        public GetHashTests()
        {
            password = null;
            salt = null;
        }

        [Fact]
        public void passed_empty_password_throw_exception()
        {
            // Act
            ArgumentException exception = null;
            try
            {
                var result = Execution();
            }
            catch (ArgumentException ex)
            {
                exception = ex;
            }
            
            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().BeEquivalentTo("Can not generate hash from an empty value.\r\nParameter name: password");
        }

        [Fact]
        public void salt_is_empty_throw_exception()
        {
            // Arrange
            password = "mySafeP@sword";

            // Act
            ArgumentException exception = null;
            try
            {
                var result = Execution();
            }
            catch (ArgumentException ex)
            {
                exception = ex;
            }
            
            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().BeEquivalentTo($"Can not use an empty salt from hashing value.\r\nParameter name: salt");
        }

        [Fact]
        public void given_valid_data_should_generate_hash()
        {
            // Arrange
            salt = Guid.NewGuid().ToString("N");
            password = "mySafeP@ssword!";

            // Act
            var result = Execution();

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void given_2_the_same_passwords_generate_identical_hashes()
        {
            // Arrange
            salt = Guid.NewGuid().ToString("N");
            password = "mySafeP@ssword!";

            // Act
            var result1 = Execution();
            var result2 = Execution();

            // Assert
            result1.Should().NotBeNullOrWhiteSpace();
            result2.Should().NotBeNullOrWhiteSpace();
            result1.Should().BeEquivalentTo(result2);
        }

        private string Execution()
        {
            var encrypter = new YourBudget.Infrastructure.Services.Encrypter();
            return encrypter.GetHash(password, salt);
        }
    }
}