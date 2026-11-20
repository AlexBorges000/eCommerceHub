using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Api.Security.Authentication.Interfaces;
using BlazorShop.Api.Security.Password.Intefaces;
using BlazorShop.Api.Services.Auth.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UserDtos.Registration;
using BlazorShop.Models.DTOs.UserDtos.Login;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlazorShop.Api.Security.Authentication;

public class AuthService(IUserRepository userRepository,
                         IPasswordService passwordService,
                         IConfiguration configuration,
                         IRefreshTokensRepository refreshTokensRepository,
                         IRoleService roleService) : IAuthService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordService _passwordService = passwordService;
    private readonly IConfiguration _configuration = configuration;
    private readonly IRefreshTokensRepository _refreshTokensRepo = refreshTokensRepository;
    private readonly IRoleService _roleService = roleService;

    public async Task<OperationResult<ResponseLoginDto>> LoginAsync(RequestLoginDto loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user is null)
            return OperationResult<ResponseLoginDto>.Fail("EMAIL OU SENHA INVALIDOS");

        if (_passwordService
            .IsInvalidPassword(user: user,
            hashedPassword: user.Password,
            password: loginDto.Password))
        {
            return OperationResult<ResponseLoginDto>.Fail("EMAIL OU SENHA INVALIDOS");
        }

        return OperationResult<ResponseLoginDto>
            .Ok(await CreateSessionAsync(user));
    }

    public async Task<ResponseLoginDto> CreateSessionAsync(User user)
    {
        var role = await FindRole(user.RoleId);

        var refreshToken = CreateRefreshToken(user.Id);

        await _refreshTokensRepo.AddAsync(refreshToken);

        return new ResponseLoginDto
        {
            Token = GenerateJwt(user, role),
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

    private string GenerateJwt(User user, string roleName)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
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

    private RefreshTokens CreateRefreshToken(int userId)
    {
        return new RefreshTokens
        {
            UserId = userId,
            Token = GenerateRefreshToken(),
            ExpirationDate = DateTime.UtcNow.AddDays(30)
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
        if (storedToken.ExpirationDate < DateTime.UtcNow)
        {
            await _refreshTokensRepo.RevokeAsync(refreshToken);
            return OperationResult<ResponseLoginDto>.Fail("Token expirado!");
        }
        var user = await _userRepository.GetByIdAsync(storedToken.UserId);
        if (user is null)
        {
            return OperationResult<ResponseLoginDto>.Fail("Usuario não encontrado!");
        }

        var role = await FindRole(user.RoleId);
        var jwt = GenerateJwt(user, role);

        var newRefreshToken = CreateRefreshToken(user.Id);
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
