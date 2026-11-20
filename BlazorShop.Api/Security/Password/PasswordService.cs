using BlazorShop.Api.Entities;
using BlazorShop.Api.Security.Password.Intefaces;
using Microsoft.AspNetCore.Identity;

namespace BlazorShop.Api.Security.Password;

public class PasswordService : IPasswordService
{
    private readonly PasswordHasher<User> _hasher;

    public PasswordService()
    {
        _hasher = new PasswordHasher<User>();
    }

    public string HashedPassword(User user, string password)
    {
        return _hasher.HashPassword(user, password);
    }

    public bool IsInvalidPassword(User user, string hashedPassword, string password)
    {
     
        return _hasher.VerifyHashedPassword(user,
        hashedPassword,
        password) == PasswordVerificationResult.Failed;
    }
  
}
