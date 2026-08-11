using BlazorShop.Api.Security.Authentication.Interfaces;
using BlazorShop.Models.DTOs.TokenDto;
using BlazorShop.Models.DTOs.UsuarioDtos.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BlazorShop.Api.Controllers;

[Route("api/usuario/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [EnableRateLimiting("LoginLimiter")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> RequestToken(RequestLoginDto requestLoginDto)
    {
        var response = await _authService.LoginAsync(requestLoginDto);
        if (response is null)
        {
            return BadRequest("Senha ou email invalidos");
        }
        if (!response.Success)
        {
            return BadRequest("Senha ou email invalidos");
        }
        return Ok(response.Value);
    }

    [EnableRateLimiting("RefreshTokenLimiter")]
    [AllowAnonymous]
    [HttpPost("Refresh")]
    public async Task<IActionResult> RefreshToken([FromBody]RequestRefreshTokenDto requestRefreshTokenDto)
    {
        var response = await _authService.RefreshAsync(requestRefreshTokenDto.RefreshToken);

        if (!response.Success)
            return Unauthorized(response);

        return Ok(response);
    }
}
