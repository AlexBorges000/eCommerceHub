using BlazorShop.Api.Entities;

namespace BlazorShop.Api.Security.Password.Intefaces;

public interface IPasswordService
{
    string HashedPassword(User user, string password);
    bool IsInvalidPassword(User user, string hashedPassword, string password);
}
