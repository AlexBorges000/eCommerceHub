using BlazorShop.Api.Services.Interfaces;
using BlazorShop.Models.DTOs.TokenDto;
using BlazorShop.Models.DTOs.UsuarioDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/usuario/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> RequestToken([FromBody] RequestLoginDto requestLoginDto)
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
    [AllowAnonymous]
    [HttpPost("Refresh")]
    public async Task<IActionResult> RefreshToken(RequestRefreshTokenDto requestRefreshTokenDto)
    {
        var response = await _authService.RefreshAsync(requestRefreshTokenDto.RefreshToken);
        if (!response.Success)
            return Unauthorized(response);
        return Ok(response);
    }
}
