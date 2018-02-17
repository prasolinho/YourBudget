using System;

namespace YourBudget.Core.Domain
{
    public class User
    {
        public Guid Id { get; protected set; }        
        public string Email { get; protected set; }
        public string UserName { get; protected set; }
        public string Name { get; protected set; }

        public string Password { get; protected set; }
        public string Salt { get; protected set; }

        public DateTime CreatedAd { get; protected set; }

        protected User()
        {
            
        }

        public User(string email, string userName, string name, string password, string salt)
        {
            Email = email;
            UserName = userName;
            Name = name;
            Password = password;
            Salt = salt;
            CreatedAd = DateTime.UtcNow;
        }
    }
}