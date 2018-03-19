using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using YourBudget.Core.Domain;
using YourBudget.Core.Repositories;
using YourBudget.Infrastructure.DTO;
using YourBudget.Infrastructure.Exceptions;

namespace YourBudget.Infrastructure.Services
{
    public class UserService : IUserService, IService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UserService> logger;
        private readonly IEncrypter encrypter;

        public UserService(IUserRepository userRepository, IEncrypter encrypter, IMapper mapper, ILogger<UserService> logger)
        {
            this.encrypter = encrypter;
            this.mapper = mapper;
            this.logger = logger;
            this.userRepository = userRepository;
        }

        // TODO: add unit test to this method
        public async Task<UserDto> GetAsync(string email)
        {
            var user = await userRepository.GetAsync(email);
            logger.LogTrace($"GetAsync: return user with email {email}");
            return mapper.Map<User, UserDto>(user);
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await userRepository.GetAsync(email); 
            if (user == null)
            {
                logger.LogTrace("Login: User with passed email doesn't exists");
                throw new ServiceException(ErrorCodes.InvalidCredentials, "Invalid credentials");
            }

            var hash = encrypter.GetHash(password, user.Salt);
            if (user.Password == hash) return true;

            logger.LogTrace("Login: invalid password");
            throw new ServiceException(ErrorCodes.InvalidCredentials, "Invalid credentials");
        }

        public async Task RegisterAsync(Guid userId, string email, string username, string password, string role)
        {
            var user = await userRepository.GetAsync(email);
            if (user != null)
            {
                throw new ServiceException(ErrorCodes.EmailInUse, $"User with email: '{email}' already exists.");
            }

            var salt = encrypter.GetSalt();
            var hash = encrypter.GetHash(password, salt);
            user = new User(userId, email, username, hash, salt, role);
            await userRepository.AddAsync(user);
        }
    }
}