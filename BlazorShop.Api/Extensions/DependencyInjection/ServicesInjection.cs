using BlazorShop.Api.Entities;
using BlazorShop.Api.Security.Authentication;
using BlazorShop.Api.Security.Authentication.Interfaces;
using BlazorShop.Api.Security.Password;
using BlazorShop.Api.Security.Password.Intefaces;
using BlazorShop.Api.Security.Security;
using BlazorShop.Api.Security.Security.Interfaces;
using BlazorShop.Api.Services.Address;
using BlazorShop.Api.Services.Address.Interfaces;
using BlazorShop.Api.Services.Auth;
using BlazorShop.Api.Services.Auth.Interfaces;
using BlazorShop.Api.Services.Users;
using BlazorShop.Api.Services.Users.Interfaces;
using BlazorShop.Api.Validations;
using BlazorShop.Models.DTOs.UserDtos.Registration;
using FluentValidation;

namespace BlazorShop.Api.Extensions.DependencyInjection
{
    public static class ServicesInjection
    {
        public static IServiceCollection AddServices
        (this IServiceCollection services)
        {
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAesService, AesService>();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<IBusinessUserService, BusinessUserService>();
            services.AddScoped<IIndividualUserService, IndividualUserService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddValidatorsFromAssemblyContaining<ValidationFilter>();
            return services;
        }
    }
}
