using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.TokensDto;
using BlazorShop.Models.DTOs.UsuarioDtos.Login;

namespace BlazorShop.Api.Services.Auth.Interfaces;

public interface IAuthService
{
    Task<OperationResult<ResponseLoginDto>> LoginAsync(RequestLoginDto loginDto);
    Task<OperationResult<ResponseLoginDto>> RefreshAsync(string refreshToken);
}
