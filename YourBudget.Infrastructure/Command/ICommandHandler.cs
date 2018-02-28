using System.Threading.Tasks;

namespace YourBudget.Infrastructure.Command
{
    public interface ICommandHandler<T> where T : ICommand
    {
         Task HandleAsync(T command);
    }
}