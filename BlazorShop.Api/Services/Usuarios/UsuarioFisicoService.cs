using BlazorShop.Api.Entities;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Api.Security.Password.Intefaces;
using BlazorShop.Api.Security.Security.Interfaces;
using BlazorShop.Api.Services.Auth.Interfaces;
using BlazorShop.Api.Services.Usuarios.Interfaces;
using BlazorShop.Models.Commons;
using BlazorShop.Models.DTOs.UsuarioDtos.Cadastro;
using BlazorShop.Models.DTOs.UsuarioDtos.Update;

namespace BlazorShop.Api.Services.Usuarios;

public class UsuarioFisicoService(IUsuarioRepository usuarioRepository,
                                IHashService hashService,
                                IAesService aesService,
                                IRoleService roleService,
                                IPasswordService passwordService) : IUsuarioFisicoService
{

    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IHashService _hashService = hashService;
    private readonly IAesService _aesService = aesService;
    private readonly IRoleService _roleService = roleService;
    private readonly IPasswordService _passwordService = passwordService;

    public async Task<OperationResult<UsuarioFisico>> InsertUsuarioPfAsync(RequestCadastroUsuarioPfDto requestCadastroUsuarioPfDto)
    {
        var role = (await _roleService.GetClienteRoleAsync()).Value ??
            throw new InvalidOperationException("A role padrão 'Cliente' não foi encontrada."); ;

        if (requestCadastroUsuarioPfDto.Senha != requestCadastroUsuarioPfDto.ConfirmaSenha)
        {
            return OperationResult<UsuarioFisico>.Fail("As senhas não coincidem");
        }

        if (await CpfJaExiste(requestCadastroUsuarioPfDto.CPF))
        {
            return OperationResult<UsuarioFisico>.Fail("CPF ja cadastrado!");
        }

        var usuarioFisico = CriarUsuarioFisico(requestCadastroUsuarioPfDto);

        var documento = ProtectDocumento(requestCadastroUsuarioPfDto.CPF);

        usuarioFisico.HashCpf = documento.Hash;
        usuarioFisico.EncryptCpf = documento.Encrypt;

        PrepararUsuario(usuarioFisico, role, requestCadastroUsuarioPfDto.Senha);

        await _usuarioRepository.AddAsync(usuarioFisico);
        return OperationResult<UsuarioFisico>.Ok(usuarioFisico);
    }

    private UsuarioFisico CriarUsuarioFisico(RequestCadastroUsuarioPfDto dto)
    {
        return new UsuarioFisico
        {
            Email = dto.Email,
           // Endereco = dto.Endereco,
            Nome = dto.Nome,
            Telefone = dto.Telefone
        };
    }

    private (string Hash, string Encrypt) ProtectDocumento(string documento)
    {
        var hash = _hashService.GetHash(documento);
        var encrypt = _aesService.Encrypt(documento);

        return (hash, encrypt);
    }

    private async Task<bool> CpfJaExiste(string documento)
    {
        var hash = _hashService.GetHash(documento);

        return await _usuarioRepository.GetByCpfHashAsync(hash) != null;
    }

    private void PrepararUsuario(Usuario usuario, Role role, string senha)
    {
        usuario.Carrinho = new Carrinho();
        usuario.Role = role;
        usuario.Senha = _passwordService.HashedPassword(usuario, senha);
    }

    public async Task<OperationResult<UsuarioFisico>> UpdateUsuarioPfAsync(int id, RequestUpdateUsuarioPfDto updateCadastroPfUsuario)
    {
        var usuario = await _usuarioRepository.GetPfByIdAsync(id);
        if (usuario is null)
        {
            return OperationResult<UsuarioFisico>.Fail("Falha ao encontrar o Usuario");
        }
       //usuario.Endereco = updateCadastroPfUsuario.Endereco ?? usuario.Endereco;
        usuario.Telefone = updateCadastroPfUsuario.Telefone ?? usuario.Telefone;
        usuario.Email = updateCadastroPfUsuario.Email ?? usuario.Email;

        await _usuarioRepository.UpdateAsync(usuario);
        return OperationResult<UsuarioFisico>.Ok(usuario);
    }

}
