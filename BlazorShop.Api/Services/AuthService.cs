using BlazorShop.Api.Entities;
using BlazorShop.Api.Migrations;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Api.Services.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.TokenDto;
using BlazorShop.Models.DTOs.UsuarioDtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlazorShop.Api.Services;

public class AuthService(IUsuarioRepository usuarioRepository,
                         IPasswordService passwordService,
                         IConfiguration configuration,
                         IRefreshTokensRepository refreshTokensRepository) : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IPasswordService _passwordService = passwordService;
    private readonly IConfiguration _configuration = configuration;
    private readonly IRefreshTokensRepository _refreshTokensRepo = refreshTokensRepository;

    public async Task<OperationResult<ResponseLoginDto>> LoginAsync(RequestLoginDto loginDto)
    {
        var usuario = await _usuarioRepository.GetUsuario(loginDto.Email);
        if (usuario is null)
            return OperationResult<ResponseLoginDto>.Fail("EMAIL OU SENHA INVALIDOS");
        if (_passwordService.IsInvalidPassword(usuario: usuario, hashedPassword: usuario.Senha, password: loginDto.Senha))
        {
            return OperationResult<ResponseLoginDto>.Fail("EMAIL OU SENHA INVALIDOS");
        }

        var refreshToken = CreateRefreshToken(usuario.Id);

        var saveRefreshToken = await _refreshTokensRepo.AddAsync(refreshToken);
        if (!saveRefreshToken.Success)
        {
            return OperationResult<ResponseLoginDto>.Fail("Refresh Token não criado");
        }
        var response = new ResponseLoginDto
        {
            Token = GenerateJwt(usuario),
            RefreshToken = refreshToken.Token
        };
        return OperationResult<ResponseLoginDto>.Ok(response, message: "Usuario Autentificado!");
    }

    private string GenerateJwt(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Email),
            new Claim(ClaimTypes.Role, "user")
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(s: _configuration["Jwt:Key"] 
            ?? throw new InvalidOperationException("Jwt:Key não configurado.")));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("Jwt:Issuer não configurado."),
            audience: _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("Jwt:Audience não configurado."),
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds);

        var response = new JwtSecurityTokenHandler().WriteToken(token);
        return response;
    }

    private string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(randomBytes);
    }

    private RefreshTokens CreateRefreshToken(int usuarioId)
    {
        return new RefreshTokens
        {
            UsuarioId = usuarioId,
            Token = GenerateRefreshToken(),
            DataExpiracao = DateTime.UtcNow.AddDays(30)
        };
    }
}
