using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace BlazorShop.Api.Extensions.Polices;

public static class CadastroRateLimitePolicy
{
    public static void AddRateLimites(RateLimiterOptions options)
    {
        options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        options.AddPolicy("CadastroLimiter", httpContext =>
        {
            var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknow";
            return RateLimitPartition.GetFixedWindowLimiter(partitionKey: ip,
                factory: _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 5,
                    QueueLimit = 0,
                    Window = TimeSpan.FromHours(1)
                });
        });
        options.OnRejected = async (context, cancellationToken) =>
        {
            await context.HttpContext.Response.WriteAsJsonAsync(
                new
                {
                    message = $"Muitas tentativas de cadastro. Tente novamente mais tarde."
                }, cancellationToken);
        };
    }
}
