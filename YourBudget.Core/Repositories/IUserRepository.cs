using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YourBudget.Core.Domain;

namespace YourBudget.Core.Repositories
{
    public interface IUserRepository : IRepository
    {
         Task AddAsync(User user);
         Task<User> GetAsync(Guid id);
         Task<User> GetAsync(string email);
         Task<IEnumerable<User>> GetAllAsync();
         Task RemoveAsync(Guid id);
         Task UpdateAsync(User user);
    }
}