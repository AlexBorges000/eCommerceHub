using BlazorShop.Api.Services.Interfaces;
using BlazorShop.Models.DTOs.UsuarioDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Api.Controllers;

[Route("api/usuario/[controller]")]
public class AuthController(IConfiguration configuration,
    IAuthService authService) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration;
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
}
