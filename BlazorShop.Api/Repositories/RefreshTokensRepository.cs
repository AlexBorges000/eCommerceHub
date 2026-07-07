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

    public async Task<RefreshTokens?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == token);
    }

    public async Task<RefreshTokens?> RevokeAsync(string token)
    {
        var foundToken = await _context.RefreshTokens.FirstOrDefaultAsync(x=> x.Token == token);
        if (foundToken is null) 
        {
            return null;
        }
        foundToken.IsRevoked = true;
        await _context.SaveChangesAsync();
        return foundToken;
    }
}
