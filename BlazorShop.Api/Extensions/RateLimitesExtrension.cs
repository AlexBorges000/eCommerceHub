using BlazorShop.Api.Extensions.Polices;

namespace BlazorShop.Api.Extensions
{
    public static class RateLimitesExtrension
    {
        public static IServiceCollection AddRateLimites(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                LoginRateLimitPolicy.AddRateLimites(options);
                CadastroRateLimitePolicy.AddRateLimites(options);
                PasswordChangePolicy.AddRateLimites(options);
            });
            return services;
        }
    }

}
