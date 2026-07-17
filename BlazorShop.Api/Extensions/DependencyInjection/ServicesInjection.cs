using BlazorShop.Api.Security.Authentication;
using BlazorShop.Api.Security.Authentication.Interfaces;
using BlazorShop.Api.Security.Password;
using BlazorShop.Api.Security.Password.Intefaces;
using BlazorShop.Api.Security.Security;
using BlazorShop.Api.Security.Security.Interfaces;
using BlazorShop.Api.Services.Auth;
using BlazorShop.Api.Services.Auth.Interfaces;
using BlazorShop.Api.Services.Usuarios;
using BlazorShop.Api.Services.Usuarios.Interfaces;

namespace BlazorShop.Api.Extensions.DependencyInjection
{
    public static class ServicesInjection
    {
        public static IServiceCollection AddServices
        (this IServiceCollection services)
        {
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAesService, AesService>();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<IUsuarioJuridicoService, UsuarioJuridicoService>();
            services.AddScoped<IUsuarioFisicoService, UsuarioFisicoService>();
            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUser>();

            return services;
        }
    }
}
