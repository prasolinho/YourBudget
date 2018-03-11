using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourBudget.Core.Domain;
using YourBudget.Core.Repositories;

namespace YourBudget.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository 
    {
        private static ISet<User> users = new HashSet<User>
        {
            new User("user1@email.com", "user1", "password1", "salt1", "user"),
            new User("user2@email.com", "user2", "password2", "salt2", "user"),
            new User("admin@email.com", "admin", "admin", "admin", "admin"),
        };

        public async Task<User> GetAsync(Guid id)
            => await Task.FromResult(users.SingleOrDefault(u => u.Id == id));

        public async Task<User> GetAsync(string email)
            => await Task.FromResult(users.SingleOrDefault(u => u.Email == email.ToLowerInvariant()));

        public async Task AddAsync(User user) 
            => await Task.FromResult(users.Add(user));

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Task.FromResult(users);

        public async Task RemoveAsync(Guid id)
        {
            var user = await GetAsync(id);
            users.Remove(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            // dopóki nie mamy bazy to nic się nie dzieje :)
            await Task.CompletedTask;
        }
    }
}