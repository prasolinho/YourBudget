using System;
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

        public UserDto Get(string email)
        {
            var user = userRepository.Get(email);
            return mapper.Map<User, UserDto>(user);
        }

        public void Register(string email, string username, string password)
        {
            var user = userRepository.Get(email);
            if (user != null)
            {
                throw new Exception($"User with email: '{email}' already exists.");
            }

            var salt = Guid.NewGuid().ToString("N");
            user = new User(email, username, password, salt);
            userRepository.Add(user);
        }
    }
}