using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using System.Threading.RateLimiting;

namespace BlazorShop.Api.Extensions.Polices;

public static class RefreshTokenRateLimitePolicy
{
    public static void AddRateLimites(RateLimiterOptions options)
    {
        options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        options.AddPolicy("RefreshTokenLimiter", httpContext =>
        {
            var id = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return RateLimitPartition.GetFixedWindowLimiter(partitionKey: id,
                factory: _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 20,
                    QueueLimit = 0,
                    Window = TimeSpan.FromMinutes(10)
                });
        });



    }
}
