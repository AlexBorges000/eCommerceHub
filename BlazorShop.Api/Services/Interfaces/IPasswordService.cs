using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Services.Interfaces;

public interface IPasswordService
{
    string HashedPassword(Usuario usuario, string password);
    bool IsInvalidPassword(Usuario usuario, string hashedPassword, string password);
    bool IsStrongPassword(string password);
}
