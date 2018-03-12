using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourBudget.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService userService;
        private readonly ILogger<DataInitializer> logger;

        public DataInitializer(IUserService userService, ILogger<DataInitializer> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        public async Task SeedAsync()
        {
            logger.LogTrace("Initializing data...");

            var tasks = new List<Task>(10);
            for (int i = 1; i <= 10; i++)
            {
                var userId = Guid.NewGuid();
                var userName = $"user{i}";

                tasks.Add(userService.RegisterAsync(userId, $"{userName}@email.com", userName, "secret", "user"));
            }
            await Task.WhenAll(tasks);

            logger.LogTrace("Data was initialized.");
        }
    }
}
