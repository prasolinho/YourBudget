using System.Threading.Tasks;
using YourBudget.Infrastructure.Command;
using YourBudget.Infrastructure.Command.Accounts;
using YourBudget.Infrastructure.Services;

namespace YourBudget.Infrastructure.Handlers.Accounts
{
    public class LogInHandler : ICommandHandler<LogIn>
    {
        private readonly IUserService userService;
        private readonly IJwtHandler jwtHandler;

        public LogInHandler(IUserService userService, IJwtHandler jwtHandler)
        {
            this.jwtHandler = jwtHandler;
            this.userService = userService;
        }

        public async Task HandleAsync(LogIn command)
        {
            await userService.LoginAsync(command.Email, command.Password);
            var jwt = jwtHandler.CreateToken(command.Email, "user"); // Na razie będzie tylko taka rola.
        }
    }
}