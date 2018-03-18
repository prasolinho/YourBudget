using System;
using System.Text.RegularExpressions;
using YourBudget.Core.Domain.Exceptions;

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

        public string Role { get; protected set; }

        public DateTime CreatedAd { get; protected set; }

        protected User()
        {
            
        }

        public User(Guid userId, string email, string userName, string password, string salt, string role)
        {
            Id = userId;
            SetEmail(email);
            SetUserName(userName);
            SetPassword(password);
            SetSalt(salt);
            SetRole(role);
            
            CreatedAd = DateTime.UtcNow;
        }

        private void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException(ErrorCodes.InvalidEmail, "Email can not be empty");
            }
           
            if (!IsEmailValid(email))
            {
                throw new DomainException(ErrorCodes.InvalidEmail, "Email is not valid");
            }
            Email = email.ToLowerInvariant();
        }

        private void SetUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new DomainException(ErrorCodes.InvalidUsername, "User name can not be empty");
            }
            // Skoro metoda jest private to na razie jest to niepotrzbne
            // if (UserName == userName)
            // {
            //     return;
            // }
            UserName = userName;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
        }

        private void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))            
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password can not be null");
            }
            if (password.Length < 4)
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password must contain at least 4 characters.");
            }
            if (password.Length > 100)
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password can not contain more than 100 characters.");
            }
            Password = password;
        }

        private void SetSalt(string salt)
        {
            if (string.IsNullOrWhiteSpace(salt))            
            {
                throw new ArgumentNullException(nameof(salt));
            }
            Salt = salt;
        }

        private void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentNullException(nameof(role));
            }
            Role = role;
        }

        private bool IsEmailValid(string email)
        {
            try 
            {
                return Regex.IsMatch(email,
                        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                        RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException) 
            {
                return false;
            }
        }

    }
}