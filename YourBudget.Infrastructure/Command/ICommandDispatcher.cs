using System.Threading.Tasks;

namespace YourBudget.Infrastructure.Command
{
    public interface ICommandDispatcher
    {
         Task DispatchAsync<T>(T command) where T : ICommand;
    }
}