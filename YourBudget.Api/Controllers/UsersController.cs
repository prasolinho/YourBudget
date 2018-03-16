using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YourBudget.Infrastructure.Command;
using YourBudget.Infrastructure.Command.Users;
using YourBudget.Infrastructure.DTO;
using YourBudget.Infrastructure.Services;

namespace YourBudget.Api.Controllers
{
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger logger;

        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher, ILogger<UsersController> logger)
            : base(commandDispatcher)
        {
            this.userService = userService;
            this.logger = logger;
        }

        //[Authorize(Policy = "admin")]
        [HttpGet("{email}")]
        public async Task<IActionResult> GetAsync(string email)
        {
            logger.LogTrace("UsersController => GetAsync => Start");
            var user = await userService.GetAsync(email);
            if (user == null)
            {
                logger.LogTrace("UsersController => GetAsync => user not found");
                return NotFound();
            }

            logger.LogTrace("UsersController => GetAsync => OK");
            return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody]CreateUser command)
        {
            logger.LogTrace("UsersController => GetAsync => Start");
            await DispatchAsync(command);
            
            return Created($"users/{command.Email}", null);
        }
    }
}
