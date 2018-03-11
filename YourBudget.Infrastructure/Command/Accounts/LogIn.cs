using System;

namespace YourBudget.Infrastructure.Command.Accounts
{
    public class LogIn : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid TokenId { get; set; }
    }
}