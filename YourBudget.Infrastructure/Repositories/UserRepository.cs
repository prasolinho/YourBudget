using System;
using System.Collections.Generic;
using System.Linq;
using YourBudget.Core.Domain;
using YourBudget.Core.Repositories;

namespace YourBudget.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ISet<User> users = new HashSet<User>
        {
            new User("user1@email.com", "user1", "password1", "salt1"),
            new User("user2@email.com", "user2", "password2", "salt2"),
            new User("user3@email.com", "user3", "password3", "salt3"),
        };

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