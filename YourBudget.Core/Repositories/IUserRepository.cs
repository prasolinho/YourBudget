using System;
using System.Collections.Generic;
using YourBudget.Core.Domain;

namespace YourBudget.Core.Repositories
{
    public interface IUserRepository
    {
         void Add(User user);
         User Get(Guid id);
         User Get(string email);
         IEnumerable<User> GetAll();
         void Remove(Guid id);
         void Update(User user);
    }
}