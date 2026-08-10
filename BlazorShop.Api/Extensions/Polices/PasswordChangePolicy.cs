using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace BlazorShop.Api.Extensions.Polices
{
    public class PasswordChangePolicy
    {
        public static void AddRateLimites(RateLimiterOptions options)
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddPolicy("PasswordChangesLimiter", httpContext =>
            {
                var ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknow";

                return RateLimitPartition.GetFixedWindowLimiter(partitionKey: ip,
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 1,
                        QueueLimit = 0,
                        Window = TimeSpan.FromMinutes(5)
                    });

            });
            options.OnRejected = async (context, cancellationToken) =>
            {
                await context.HttpContext.Response.WriteAsJsonAsync(
                    new
                    {
                        message = $"Há uma troca de senha em andamento..."
                    }, cancellationToken);
            };
        }
    }
}
