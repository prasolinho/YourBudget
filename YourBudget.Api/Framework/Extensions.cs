using Microsoft.AspNetCore.Builder;

namespace YourBudget.Api.Framework
{
    public static class Extensions
    {
        public static IApplicationBuilder UseCustomeExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
