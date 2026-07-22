using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Api.Security.Password.Intefaces;
using BlazorShop.Api.Services.Usuarios.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos.Update;

namespace BlazorShop.Api.Services.Usuarios;

public class UsuarioService(IPasswordService passwordService,
                            IUsuarioRepository usuarioRepository) : IUsuarioService
{
    private readonly IPasswordService _passwordService = passwordService;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

    public async Task<OperationResult<Usuario>> ChangePassword(int id, RequestUpdateSenhaUsuarioDto updateSenhaUsuarioDto)
    {
        if (updateSenhaUsuarioDto.ConfirmedPassword != updateSenhaUsuarioDto.NewPassword)
        {
            return OperationResult<Usuario>.Fail("As senhas não coincidem");
        }

        var usuario = await _usuarioRepository.GetByIdAsync(id);

        if (usuario is null)
        {
            return OperationResult<Usuario>.Fail("Usuario Não encontrado");
        }
        if (_passwordService.IsInvalidPassword(usuario,
            hashedPassword: usuario.Senha,
            password: updateSenhaUsuarioDto.OldPassword))
        {
            return OperationResult<Usuario>.Fail("A senha antiga esta errada");
        }
        var hashedPassword = _passwordService.HashedPassword(usuario, updateSenhaUsuarioDto.NewPassword);
        usuario.Senha = hashedPassword;
        await _usuarioRepository.UpdateAsync(usuario);
        return OperationResult<Usuario>.Ok(usuario);
    }

    public async Task<OperationResult<Usuario>> GetByEmailAsync(string email)
    {

        var user = await _usuarioRepository.GetByEmailAsync(email);
        if (user is null)
        {
            return OperationResult<Usuario>.Fail("Senha ou Email Invalidos");
        }
        return OperationResult<Usuario>.Ok(user);
    }

    public async Task<OperationResult<Usuario>> GetByIdAsync(int id)
    {
        var user = await _usuarioRepository.GetByIdAsync(id);
        if (user is null)
        {
            return OperationResult<Usuario>.Fail("Usuario não Cadastrado");
        }
        return OperationResult<Usuario>.Ok(user);
    }

    public async Task<OperationResult<Usuario>> DeleteUsuarioAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario is null)
        {
            return OperationResult<Usuario>.Fail("Usuario Não encontrado para deletar");
        }
        await _usuarioRepository.DeleteUsuarioAsync(usuario);
        return OperationResult<Usuario>.Ok(usuario, message: "Usuario deletado com sucesso");

    }
}
