using System;
using System.Threading.Tasks;
using AutoMapper;
using YourBudget.Core.Domain;
using YourBudget.Core.Repositories;
using YourBudget.Infrastructure.DTO;

namespace YourBudget.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        // TODO: add unit test to this method
        public async Task<UserDto> GetAsync(string email)
        {
            var user = await userRepository.GetAsync(email);
            return mapper.Map<User, UserDto>(user);
        }

        public async Task RegisterAsync(string email, string username, string password)
        {
            var user = await userRepository.GetAsync(email);
            if (user != null)
            {
                throw new Exception($"User with email: '{email}' already exists.");
            }

            var salt = Guid.NewGuid().ToString("N");
            user = new User(email, username, password, salt);
            await userRepository.AddAsync(user);
        }
    }
}