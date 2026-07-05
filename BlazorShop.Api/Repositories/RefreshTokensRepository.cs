using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Models.Commons;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Repositories;

public class RefreshTokensRepository(AppDbContext context) : IRefreshTokensRepository
{
    private readonly AppDbContext _context = context;
    public async Task<RefreshTokens> AddAsync(RefreshTokens refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
        return refreshToken;
    }

    public async Task<RefreshTokens> RefreshAsync(string token)
    {
        throw new NotImplementedException();
    }

    public async Task<RefreshTokens?> RevokeAsync(string token)
    {
        var findToken = await _context.RefreshTokens.FirstOrDefaultAsync(x=> x.Token == token);
        if (findToken is null) 
        {
            return null;
        }
        findToken.IsRevoked = true;
        await _context.SaveChangesAsync();
        return findToken;
    }
}
