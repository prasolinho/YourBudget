using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourBudget.Infrastructure.Command;
using YourBudget.Infrastructure.Command.Accounts;
using YourBudget.Infrastructure.Extensions;

namespace YourBudget.Api.Controllers
{
    public class LoginController : ApiControllerBase
    {
        private readonly IMemoryCache cache;

        public LoginController(ICommandDispatcher commandDispatcher, IMemoryCache cache) : base(commandDispatcher)
        {
            this.cache = cache;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LogIn command)
        {
            command.TokenId = Guid.NewGuid();
            await DispatchAsync(command);
            var token = cache.GetJwt(command.TokenId);

            return Json(token);
        }
    }
}
