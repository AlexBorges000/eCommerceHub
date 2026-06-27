using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BlazorShop.Api.Repositories;

public class PasswordRepository : IPasswordRepository
{
    private readonly PasswordHasher<Usuario> _hasher;

    public PasswordRepository()
    {
        _hasher = new PasswordHasher<Usuario>();
    }

    public string HashedPassword(Usuario usuario, string password)
    {
        return _hasher.HashPassword(usuario, password);
    }

    public bool VerifyPassword(Usuario usuario, string hashedPassword, string password)
    {
        return _hasher.VerifyHashedPassword(usuario,
        hashedPassword,
        password) != PasswordVerificationResult.Failed;
    }
}
