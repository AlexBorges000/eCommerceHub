using BlazorShop.Api.Repositories;
using BlazorShop.Api.Repositories.Interfaces;

namespace BlazorShop.Api.Extensions.DependencyInjection;

public static class RepositoryInjection
{
    public static IServiceCollection AddRepository(
        this IServiceCollection services)
    {
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<ICarrinhoCompraRepository, CarrinhoCompraRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IEnderecoRepository, EnderecoRepository>();

        return services;
    }

}
