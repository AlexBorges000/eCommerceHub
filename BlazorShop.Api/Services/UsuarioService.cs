using BlazorShop.Api.Entities;
using BlazorShop.Api.Entities.Enums;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Api.Services.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos;

namespace BlazorShop.Api.Services;

public class UsuarioService(IPasswordService passwordHasher,
                            IUsuarioRepository usuarioRepository,
                            IRoleService roleService) : IUsuarioService
{
    private readonly IPasswordService _passwordHasher = passwordHasher;
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IRoleService _roleService = roleService;

    public async Task<OperationResult<Usuario>> InsertAsync(RequestCadastroUsuarioDto requestCadastroUsuarioDto)
    {
        var tipoPessoa = (TipoPessoa)requestCadastroUsuarioDto.TipoPessoa;
        var role = await _roleService.GetClienteRoleAsync() ?? throw new InvalidOperationException(
        "A role padrão 'Cliente' não foi encontrada."); ;
        var roleEntity = role.Value;

        if (roleEntity == null)
        {
            throw new InvalidOperationException("A role padrão não foi encontrada.");
        }

        if (requestCadastroUsuarioDto.Senha != requestCadastroUsuarioDto.ConfirmaSenha)
        {
            return OperationResult<Usuario>.Fail("Senhas não conhecidem");
        }

        var usuario = new Usuario
        {
            CNPJ = requestCadastroUsuarioDto.CNPJ,
            CPF = requestCadastroUsuarioDto.CPF,
            Email = requestCadastroUsuarioDto.Email,
            Endereco = requestCadastroUsuarioDto.Endereco,
            InscricaoEstadual = requestCadastroUsuarioDto.InscricaoEstadual,
            Nome = requestCadastroUsuarioDto.Nome,
            NomeFantasia = requestCadastroUsuarioDto.NomeFantasia,
            RazaoSocial = requestCadastroUsuarioDto.RazaoSocial,
            ResponsavelCompra = requestCadastroUsuarioDto.ResponsavelCompra,
            Telefone = requestCadastroUsuarioDto.Telefone,
            TipoPessoa = tipoPessoa,

        };

        usuario.Carrinho = new Carrinho();
        usuario.Role = roleEntity;
        var hashPassword = _passwordHasher.HashedPassword(usuario, requestCadastroUsuarioDto.Senha);
        usuario.Senha = hashPassword;

        await _usuarioRepository.AddAsync(usuario);
        return OperationResult<Usuario>.Ok(usuario);
    }

    public async Task<OperationResult<Usuario>> UpdateAsync(int id, RequestUpdateCadastroUsuarioDto updateCadastroUsuario)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario is null)
        {
            return OperationResult<Usuario>.Fail("Falha ao encontrar o Usuario");
        }

        usuario.NomeFantasia = updateCadastroUsuario.NomeFantasia ?? usuario.NomeFantasia;
        usuario.RazaoSocial = updateCadastroUsuario.RazaoSocial ?? usuario.RazaoSocial;
        usuario.ResponsavelCompra = updateCadastroUsuario.ResponsavelCompra ?? usuario.ResponsavelCompra;
        usuario.InscricaoEstadual = updateCadastroUsuario.InscricaoEstadual ?? usuario.InscricaoEstadual;
        usuario.Endereco = updateCadastroUsuario.Endereco ?? usuario.Endereco;
        usuario.Telefone = updateCadastroUsuario.Telefone ?? usuario.Telefone;
        usuario.Email = updateCadastroUsuario.Email ?? usuario.Email;
        usuario.Nome = updateCadastroUsuario.Nome ?? usuario.Nome;

        await _usuarioRepository.UpdateAsync(usuario);
        return OperationResult<Usuario>.Ok(usuario);
    }

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
        if (_passwordHasher.IsInvalidPassword(usuario,
            hashedPassword: usuario.Senha,
            password: updateSenhaUsuarioDto.OldPassword))
        {
            return OperationResult<Usuario>.Fail("A senha antiga esta errada");
        }
        var hashedPassword = _passwordHasher.HashedPassword(usuario, updateSenhaUsuarioDto.NewPassword);
        usuario.Senha = hashedPassword;
        await _usuarioRepository.UpdateAsync(usuario);
        return OperationResult<Usuario>.Ok(usuario);
    }

    public async Task<OperationResult<Usuario>> GetAsync(string email)
    {

        var user = await _usuarioRepository.GetAsync(email);
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
