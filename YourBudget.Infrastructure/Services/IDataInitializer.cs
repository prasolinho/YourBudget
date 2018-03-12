using System.Text;
using System.Threading.Tasks;

namespace YourBudget.Infrastructure.Services
{
    public interface IDataInitializer : IService
    {
        Task SeedAsync();
    }
}
