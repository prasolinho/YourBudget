using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using YourBudget.Infrastructure.Command;
using YourBudget.Infrastructure.Command.Accounts;
using YourBudget.Infrastructure.Extensions;
using YourBudget.Infrastructure.Services;

namespace YourBudget.Infrastructure.Handlers.Accounts
{
    public class LogInHandler : ICommandHandler<LogIn>
    {
        private readonly IUserService userService;
        private readonly IJwtHandler jwtHandler;
        private readonly IMemoryCache cache;

        public LogInHandler(IUserService userService, IJwtHandler jwtHandler, IMemoryCache cache)
        {
            this.jwtHandler = jwtHandler;
            this.cache = cache;
            this.userService = userService;
        }

        public async Task HandleAsync(LogIn command)
        {
            await userService.LoginAsync(command.Email, command.Password);
            var user = await userService.GetAsync(command.Email);
            var token = jwtHandler.CreateToken(user.Id, user.Role);
            cache.SetJwt(command.TokenId, token);
        }
    }
}