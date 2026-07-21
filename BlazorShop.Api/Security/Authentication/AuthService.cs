using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Api.Security.Authentication.Interfaces;
using BlazorShop.Api.Security.Password.Intefaces;
using BlazorShop.Api.Services.Auth.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos.Cadastro;
using BlazorShop.Models.DTOs.UsuarioDtos.Login;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlazorShop.Api.Security.Authentication;

public class AuthService(IUsuarioRepository usuarioRepository,
                         IPasswordService passwordService,
                         IConfiguration configuration,
                         IRefreshTokensRepository refreshTokensRepository,
                         IRoleService roleService) : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IPasswordService _passwordService = passwordService;
    private readonly IConfiguration _configuration = configuration;
    private readonly IRefreshTokensRepository _refreshTokensRepo = refreshTokensRepository;
    private readonly IRoleService _roleService = roleService;

    public async Task<OperationResult<ResponseLoginDto>> LoginAsync(RequestLoginDto loginDto)
    {
        var usuario = await _usuarioRepository.GetByEmailAsync(loginDto.Email);
        if (usuario is null)
            return OperationResult<ResponseLoginDto>.Fail("EMAIL OU SENHA INVALIDOS");

        if (_passwordService
            .IsInvalidPassword(usuario: usuario,
            hashedPassword: usuario.Senha,
            password: loginDto.Senha))
        {
            return OperationResult<ResponseLoginDto>.Fail("EMAIL OU SENHA INVALIDOS");
        }

        return OperationResult<ResponseLoginDto>
            .Ok(await CreateSessionAsync(usuario));
    }

    public async Task<ResponseLoginDto> CreateSessionAsync(Usuario usuario)
    {
        var role = await FindRole(usuario.RoleId);

        var refreshToken = CreateRefreshToken(usuario.Id);

        await _refreshTokensRepo.AddAsync(refreshToken);

        return new ResponseLoginDto
        {
            Token = GenerateJwt(usuario, role),
            RefreshToken = refreshToken.Token
        };
    }

    private async Task<string> FindRole(int roleId)
    {
        var role = await _roleService.GetRoleAsync(roleId);

        if (role.Value is null)
        {
            throw new InvalidOperationException("A role  não foi encontrada.");
        }
        return role.Value.Name;
    }

    private string GenerateJwt(Usuario usuario, string roleName)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Email),
            new Claim(ClaimTypes.Role, roleName)
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

    public async Task<OperationResult<ResponseLoginDto>> RefreshAsync(string refreshToken)
    {
        var storedToken = await _refreshTokensRepo.GetByTokenAsync(refreshToken);

        if (storedToken is null)
        {
            return OperationResult<ResponseLoginDto>.Fail("Token não encontrado!");
        }
        if (storedToken.IsRevoked)
        {
            return OperationResult<ResponseLoginDto>.Fail("Token revogado!");
        }
        if (storedToken.DataExpiracao < DateTime.UtcNow)
        {
            await _refreshTokensRepo.RevokeAsync(refreshToken);
            return OperationResult<ResponseLoginDto>.Fail("Token expirado!");
        }
        var usuario = await _usuarioRepository.GetByIdAsync(storedToken.UsuarioId);
        if (usuario is null)
        {
            return OperationResult<ResponseLoginDto>.Fail("Usuario não encontrado!");
        }

        var role = await FindRole(usuario.RoleId);
        var jwt = GenerateJwt(usuario, role);

        var newRefreshToken = CreateRefreshToken(usuario.Id);
        await _refreshTokensRepo.AddAsync(newRefreshToken);
        await _refreshTokensRepo.RevokeAsync(storedToken.Token);

        var responseLogin = new ResponseLoginDto
        {
            RefreshToken = newRefreshToken.Token,
            Token = jwt
        };
        return OperationResult<ResponseLoginDto>.Ok(responseLogin);
    }
}
