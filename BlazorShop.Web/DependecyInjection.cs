using BlazorShop.Models.Config;
using BlazorShop.Web.Services;
using BlazorShop.Web.Services.Interfaces;

namespace BlazorShop.Web;

public static class DependecyInjection
{
    public static IServiceCollection WebRazorDepencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<ICarrinhoCompraService, CarrinhoCompraService>();
        ConfigureAddHttpClient(services);
        return services;
    }

    private static void ConfigureAddHttpClient(IServiceCollection services)
    {
        services.AddHttpClient(HttpConfiguration.Compras, opts =>
        {
            opts.BaseAddress = new Uri(HttpConfiguration.BaseUrl);
            opts.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddHttpClient(HttpConfiguration.Produtos, opts =>
        {
            opts.BaseAddress = new Uri(HttpConfiguration.BaseUrl);
            opts.Timeout = TimeSpan.FromSeconds(30);
        });
    }
}
