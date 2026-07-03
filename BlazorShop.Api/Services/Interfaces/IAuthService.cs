using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos;

namespace BlazorShop.Api.Services.Interfaces;

public interface IAuthService
{
    Task<OperationResult<LoginResponseDto>> LoginAsync(RequestLoginDto loginDto);
}
