using System;
using System.Threading.Tasks;
using AutoMapper;
using YourBudget.Core.Domain;
using YourBudget.Core.Repositories;
using YourBudget.Infrastructure.DTO;

namespace YourBudget.Infrastructure.Services
{
    public class UserService : IUserService, IService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IEncrypter encrypter;

        public UserService(IUserRepository userRepository, IEncrypter encrypter, IMapper mapper)
        {
            this.encrypter = encrypter;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        // TODO: add unit test to this method
        public async Task<UserDto> GetAsync(string email)
        {
            var user = await userRepository.GetAsync(email);
            return mapper.Map<User, UserDto>(user);
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await userRepository.GetAsync(email); 
            if (user == null)
            {
                throw new Exception("Invalid credentials");
            }

            var hash = encrypter.GetHash(password, user.Salt);
            if (user.Password == hash) return true;

            throw new Exception("Invalid credentials");
        }

        public async Task RegisterAsync(string email, string username, string password)
        {
            var user = await userRepository.GetAsync(email);
            if (user != null)
            {
                throw new Exception($"User with email: '{email}' already exists.");
            }

            var salt = encrypter.GetSalt();
            var hash = encrypter.GetHash(password, salt);
            user = new User(email, username, hash, salt);
            await userRepository.AddAsync(user);
        }
    }
}