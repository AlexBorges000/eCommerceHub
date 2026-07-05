using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Api.Services.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos;

namespace BlazorShop.Api.Services;

public class AuthService(IUsuarioRepository usuarioRepository, IPasswordService passwordService) : IAuthService
{
    IUsuarioRepository _usuarioRepository = usuarioRepository;
    IPasswordService _passwordService = passwordService;
    public async Task<OperationResult<RequestLoginDto>> LoginAsync(RequestLoginDto loginDto)
    {
        var usuario = await _usuarioRepository.GetUsuario(loginDto.Email);
        if (usuario is null)
            return OperationResult<RequestLoginDto>.Fail("EMAIL OU SENHA INVALIDOS");
        if(!_passwordService.VerifyPassword(usuario: usuario, hashedPassword: usuario.Senha, password: loginDto.Senha))
        {
            return OperationResult<RequestLoginDto>.Fail("EMAIL OU SENHA INVALIDOS");
        }
        
        
    }
}
