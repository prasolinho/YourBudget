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
        public async Task<IActionResult> RegisterAsync([FromBody]CreateUser request)
        {
            await userService.RegisterAsync(request.Email, request.UserName, request.Password);
            return Created($"users/{request.Email}", new object());
        }
    }
}
