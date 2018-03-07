using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourBudget.Infrastructure.Command;
using YourBudget.Infrastructure.Command.Users;
using YourBudget.Infrastructure.DTO;
using YourBudget.Infrastructure.Services;

namespace YourBudget.Api.Controllers
{
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher)
            : base(commandDispatcher)
        {
            this.userService = userService;

        }

        //[Authorize(Policy = "admin")]
        [HttpGet("{email}")]
        public async Task<IActionResult> GetAsync(string email)
        {
            var user = await userService.GetAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody]CreateUser command)
        {
            await CommandDispatcher.DispatchAsync(command);
            
            return Created($"users/{command.Email}", new object());
        }
    }
}
