using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YourBudget.Infrastructure.Command.Users;
using YourBudget.Infrastructure.DTO;
using YourBudget.Infrastructure.Services;

namespace YourBudget.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;

        }

        // GET api/values/5
        [HttpGet("{email}")]
        public async Task<UserDto> GetAsync(string email)
            => await userService.GetAsync(email);

        [HttpPost]
        public async Task RegisterAsync([FromBody]CreateUser user)
        {
            await userService.RegisterAsync(user.Email, user.UserName, user.Password);
        }
    }
}
