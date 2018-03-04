using YourBudget.Infrastructure.DTO;

namespace YourBudget.Infrastructure.Services
{
    public interface IJwtHandler
    {
         JwtDto CreateToken(string email, string role);
    }
}