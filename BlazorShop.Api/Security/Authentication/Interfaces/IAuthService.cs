using BlazorShop.Api.Entities;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UserDtos.Registration;
using BlazorShop.Models.DTOs.UserDtos.Login;

namespace BlazorShop.Api.Security.Authentication.Interfaces;

public interface IAuthService
{
    Task<OperationResult<ResponseLoginDto>> LoginAsync(RequestLoginDto loginDto);
    Task<OperationResult<ResponseLoginDto>> RefreshAsync(string refreshToken);
    Task<ResponseLoginDto> CreateSessionAsync(User user);
}
