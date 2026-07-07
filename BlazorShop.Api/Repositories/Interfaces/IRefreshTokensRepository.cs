using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.TokenDto;

namespace BlazorShop.Api.Repositories.Interfaces;

public interface IRefreshTokensRepository
{
    Task<RefreshTokens> AddAsync(RefreshTokens refreshToken);
    Task<RefreshTokens?> RevokeAsync(string token);
    Task<RefreshTokens?> GetByTokenAsync(string token);
}
