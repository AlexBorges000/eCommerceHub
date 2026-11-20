using BlazorShop.Models.Config;
using BlazorShop.Web.Services;
using BlazorShop.Web.Services.Interfaces;

namespace BlazorShop.Web;

public static class DependecyInjection
{
    public static IServiceCollection WebRazorDepencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IShoppingOrderService, ShoppingOrderService>();
        services.AddScoped<IUserService, UserService>();
        ConfigureAddHttpClient(services);
        return services;
    }

    private static void ConfigureAddHttpClient(IServiceCollection services)
    {
        services.AddHttpClient(HttpConfiguration.Compras, opts =>
        {
            opts.BaseAddress = new Uri(HttpConfiguration.BaseUrl);
            opts.Timeout = TimeSpan.FromSeconds(100);
        });

        services.AddHttpClient(HttpConfiguration.Products, opts =>
        {
            opts.BaseAddress = new Uri(HttpConfiguration.BaseUrl);
            opts.Timeout = TimeSpan.FromSeconds(100);
        });
        services.AddHttpClient(HttpConfiguration.User, opts => {
            opts.BaseAddress = new Uri(HttpConfiguration.BaseUrl);
            opts.Timeout = TimeSpan.FromSeconds(100);
            });
    }
}
