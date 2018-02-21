using System;
using System.Collections.Generic;
using System.Linq;
using YourBudget.Core.Domain;
using YourBudget.Core.Repositories;

namespace YourBudget.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ISet<User> users = new HashSet<User>();

        public void Add(User user) 
            => users.Add(user);
        

        public User Get(Guid id)
            => users.Single(u => u.Id == id);

        public User Get(string email)
            => users.Single(u => u.Email == email.ToLowerInvariant());

        public IEnumerable<User> GetAll()
            => users;

        public void Remove(Guid id)
        {
            var user = Get(id);
            users.Remove(user);
        }

        public void Update(User user)
        {
            // dopóki nie mamy bazy to nic się nie dzieje :)
        }
    }
}