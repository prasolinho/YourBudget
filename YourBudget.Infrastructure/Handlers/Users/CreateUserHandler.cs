using System.Threading.Tasks;
using YourBudget.Infrastructure.Command;
using YourBudget.Infrastructure.Command.Users;
using YourBudget.Infrastructure.Services;

namespace YourBudget.Infrastructure.Handlers.Users
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IUserService userService;

        public CreateUserHandler(IUserService userService)
        {
            this.userService = userService;

        }
        public async Task HandleAsync(CreateUser command)
        {
            await userService.RegisterAsync(command.Email, command.UserName, command.Password);
        }
    }
}