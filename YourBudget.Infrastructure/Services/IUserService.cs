using System;
using System.Threading.Tasks;
using YourBudget.Infrastructure.DTO;

namespace YourBudget.Infrastructure.Services
{
    public interface IUserService : IService
    {
         Task RegisterAsync(Guid userId, string email, string username, string password, string role);

         Task<UserDto> GetAsync(string email);

         Task<bool> LoginAsync(string email, string password);
    }
}