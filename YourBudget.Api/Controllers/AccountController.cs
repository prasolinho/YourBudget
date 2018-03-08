using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YourBudget.Infrastructure.Command;
using YourBudget.Infrastructure.Command.Accounts;
using YourBudget.Infrastructure.Services;

namespace YourBudget.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IJwtHandler jwtHandler;
        public AccountController(ICommandDispatcher commandDispatcher, IJwtHandler jwtHandler)
            : base(commandDispatcher)
        {
            this.jwtHandler = jwtHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Post([FromBody] LogIn command)
        {
            await CommandDispatcher.DispatchAsync(command);
            return Json("Not throwing exception so must be OK.");
        }
    }
}