using BlazorShop.Api.Entities;
using BlazorShop.Api.Security.Password.Intefaces;
using Microsoft.AspNetCore.Identity;

namespace BlazorShop.Api.Security.Password;

public class PasswordService : IPasswordService
{
    private readonly PasswordHasher<Usuario> _hasher;

    public PasswordService()
    {
        _hasher = new PasswordHasher<Usuario>();
    }

    public string HashedPassword(Usuario usuario, string password)
    {
        return _hasher.HashPassword(usuario, password);
    }

    public bool IsInvalidPassword(Usuario usuario, string hashedPassword, string password)
    {
     
        return _hasher.VerifyHashedPassword(usuario,
        hashedPassword,
        password) == PasswordVerificationResult.Failed;
    }
  
}
