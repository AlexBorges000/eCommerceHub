using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Services.Security.Interfaces;

public interface IPasswordService
{
    string HashedPassword(Usuario usuario, string password);
    bool IsInvalidPassword(Usuario usuario, string hashedPassword, string password);
}
