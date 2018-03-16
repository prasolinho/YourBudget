using System;
using System.Threading.Tasks;
using YourBudget.Core.Domain;
using YourBudget.Core.Repositories;

namespace YourBudget.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<User> GetOrFailAsync(this IUserRepository userRepository, Guid userId)
        {
            var user = await userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new Exception("User doesn't exitst");
            }
            return user;
        }
    }
}
