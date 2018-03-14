using System;

namespace YourBudget.Infrastructure.Command
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid UserId { get; set; }
    }
}
