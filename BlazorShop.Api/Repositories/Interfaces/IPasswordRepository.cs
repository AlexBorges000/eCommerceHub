using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface IPasswordRepository
{
    string HashedPassword(Usuario usuario, string password);
    bool VerifyPassword(Usuario usuario, string hashedPassword, string password);
}
