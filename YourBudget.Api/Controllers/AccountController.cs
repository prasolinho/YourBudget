using Microsoft.AspNetCore.Mvc;
using YourBudget.Infrastructure.Command;
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
        [Route("token")]
        public IActionResult Get()
        {
            var token = jwtHandler.CreateToken("user1@email.com", "admin");
            return Json(token);
        }
    }
}