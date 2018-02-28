using System;
using System.Threading.Tasks;
using Autofac;

namespace YourBudget.Infrastructure.Command
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext context;
        public CommandDispatcher(IComponentContext context)
        {
            this.context = context;
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command), $"Command {typeof(T).Name} can not be null.");
            }

            var handler = context.Resolve<ICommandHandler<T>>();
            await handler.HandleAsync(command);
        }
    }
}