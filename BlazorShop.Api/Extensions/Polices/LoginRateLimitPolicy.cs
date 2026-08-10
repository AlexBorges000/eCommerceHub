using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace BlazorShop.Api.Extensions.Polices;

public static class LoginRateLimitPolicy
{
    public static void AddRateLimites(RateLimiterOptions options)
    {
        options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        options.AddPolicy("LoginLimiter", httpContext =>
        {
            var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknow";

            return RateLimitPartition.GetFixedWindowLimiter(partitionKey: ip,
                factory: _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 3,
                    QueueLimit = 0,
                    Window = TimeSpan.FromMinutes(1)
                });

        });
        options.OnRejected = async (context, cancellationToken) =>
        {
            await context.HttpContext.Response.WriteAsJsonAsync(
                new
                {
                    message = $"Muitas tentativas de login. Tente novamente mais tarde."
                }, cancellationToken);
        };
    }
}