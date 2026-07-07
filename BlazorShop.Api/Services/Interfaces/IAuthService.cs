using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.TokensDto;
using BlazorShop.Models.DTOs.UsuarioDtos;

namespace BlazorShop.Api.Services.Interfaces;

public interface IAuthService
{
    Task<OperationResult<ResponseLoginDto>> LoginAsync(RequestLoginDto loginDto);
    Task<OperationResult<ResponseLoginDto>> RefreshAsync(string refreshToken);
}
