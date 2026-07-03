using BlazorShop.Api.Entities;
using BlazorShop.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BlazorShop.Api.Services;

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

    public bool IsStrongPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        if (password.Length < 8)
            return false;

        bool hasUpper = false;
        bool hasLower = false;
        bool hasDigit = false;
        bool hasSpecial = false;

        foreach (char c in password)
        {
            if (char.IsUpper(c)) hasUpper = true;
            else if (char.IsLower(c)) hasLower = true;
            else if (char.IsDigit(c)) hasDigit = true;
            else hasSpecial = true;
        }
        return hasUpper && hasLower && hasDigit && hasSpecial;
    }
}
