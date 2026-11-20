using BlazorShop.Api.Repositories;
using BlazorShop.Api.Repositories.Interfaces;

namespace BlazorShop.Api.Extensions.DependencyInjection;

public static class RepositoryInjection
{
    public static IServiceCollection AddRepository(
        this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IShoppingOrderRepository, ShoppingOrderRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();

        return services;
    }

}
