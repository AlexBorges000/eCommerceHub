using BlazorShop.Api.Entities;
using BlazorShop.Api.Mappings.Usuarios;
using BlazorShop.Api.Repositories.Interfaces;
using BlazorShop.Api.Security.Authentication.Interfaces;
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
                                IPasswordService passwordService,
                                IAuthService authService) : IUsuarioFisicoService
{

    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
    private readonly IHashService _hashService = hashService;
    private readonly IAesService _aesService = aesService;
    private readonly IRoleService _roleService = roleService;
    private readonly IPasswordService _passwordService = passwordService;
    private readonly IAuthService _authService = authService;

    public async Task<OperationResult<ResponseLoginDto>> InsertUsuarioPfAsync(RequestCadastroUsuarioPfDto requestCadastroUsuarioPfDto)
    {
        var role = (await _roleService.GetClienteRoleAsync()).Value ??
            throw new InvalidOperationException("A role padrão 'Cliente' não foi encontrada."); ;

        if (requestCadastroUsuarioPfDto.Senha != requestCadastroUsuarioPfDto.ConfirmaSenha)
        {
            return OperationResult<ResponseLoginDto>.Fail("As senhas não coincidem");
        }

        if (await CpfJaExiste(requestCadastroUsuarioPfDto.CPF))
        {
            return OperationResult<ResponseLoginDto>.Fail("CPF ja cadastrado!");
        }

        var usuarioFisico = requestCadastroUsuarioPfDto.ToEntity();

        var documento = ProtectDocumento(requestCadastroUsuarioPfDto.CPF);

        usuarioFisico.HashCpf = documento.Hash;
        usuarioFisico.EncryptCpf = documento.Encrypt;

        PrepararUsuario(usuarioFisico, role, requestCadastroUsuarioPfDto.Senha);

        await _usuarioRepository.AddAsync(usuarioFisico);

        var response = await _authService.CreateSessionAsync(usuarioFisico);

        return OperationResult<ResponseLoginDto>
            .Ok(new ResponseLoginDto
            {
                RefreshToken = response.RefreshToken,
                Token = response.Token,
            });
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

        usuario.Telefone = updateCadastroPfUsuario.Telefone ?? usuario.Telefone;
        usuario.Email = updateCadastroPfUsuario.Email ?? usuario.Email;

        await _usuarioRepository.UpdateAsync(usuario);
        return OperationResult<UsuarioFisico>.Ok(usuario);
    }

}
